using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Application.Chat;
using AiApplication.Domain.AI;
using AiApplication.Domain.Common;

namespace AiApplication.Infrastructure.AI.DeepSeek
{
    /// <summary>
    /// DeepSeek Chat Completions 客户端实现。
    /// DeepSeek 兼容 OpenAI 协议，仅 BaseUrl 与默认模型不同。
    /// </summary>
    public sealed class DeepSeekClient : IAiClient
    {
        private readonly HttpClient _httpClient;
        private readonly DeepSeekOptions _options;
        private readonly IAiResponseParser _responseParser;

        public AiProvider Provider => AiProvider.DeepSeek;

        public DeepSeekClient(HttpClient httpClient, DeepSeekOptions options, IAiResponseParser responseParser)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new System.ArgumentNullException(nameof(options));
            _responseParser = responseParser ?? throw new System.ArgumentNullException(nameof(responseParser));
        }

        public async Task<ChatResponse> ChatAsync(
            ChatRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                return ChatResponse.Failure(Error.Unauthorized("DeepSeek ApiKey 未配置。"));
            }

            var messages = BuildMessages(request);
            if (messages.Count == 0)
            {
                return ChatResponse.Failure(Error.InvalidArgument("消息列表不能为空。"));
            }

            var model = string.IsNullOrWhiteSpace(request.Model) ? _options.DefaultModel : request.Model;

            var requestJson = DeepSeekRequestMapper.Map(
                model,
                messages,
                request.Temperature ?? _options.DefaultTemperature,
                request.MaxTokens ?? _options.DefaultMaxTokens);

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "chat/completions")
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            };
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);

            try
            {
                using var response = await _httpClient
                    .SendAsync(
                        httpRequest,
                        HttpCompletionOption.ResponseHeadersRead,
                        cancellationToken);

                var raw = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return ChatResponse.Failure(
                        Error.ProviderError($"DeepSeek 调用失败: HTTP {response.StatusCode}, {raw}"));
                }

                var completion = _responseParser.Parse(raw);
                return ChatResponse.Success(completion.Content, completion.Usage);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return ChatResponse.Failure(Error.ProviderError($"DeepSeek 请求超时: {ex.Message}"));
            }
        }

        private static IReadOnlyList<AiMessage> BuildMessages(ChatRequest request)
        {
            var list = new List<AiMessage>();
            if (!string.IsNullOrWhiteSpace(request.SystemPrompt))
            {
                list.Add(AiMessage.System(request.SystemPrompt));
            }

            if (request.History != null)
            {
                foreach (var msg in request.History)
                {
                    list.Add(msg);
                }
            }

            if (!string.IsNullOrEmpty(request.UserMessage))
            {
                list.Add(AiMessage.User(request.UserMessage));
            }

            return list;
        }
    }
}
