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

namespace AiApplication.Infrastructure.AI.Claude
{
    /// <summary>
    /// Anthropic Claude Messages API 客户端实现。
    /// 调用 https://api.anthropic.com/v1/messages，需要 x-api-key 与 anthropic-version 头。
    /// </summary>
    public sealed class ClaudeClient : IAiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ClaudeOptions _options;
        private readonly IAiResponseParser _responseParser;

        public AiProvider Provider => AiProvider.Claude;

        public ClaudeClient(HttpClient httpClient, ClaudeOptions options, IAiResponseParser responseParser)
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
                return ChatResponse.Failure(Error.Unauthorized("Claude ApiKey 未配置。"));
            }

            var messages = BuildMessages(request);
            if (messages.Count == 0)
            {
                return ChatResponse.Failure(Error.InvalidArgument("消息列表不能为空。"));
            }

            var model = string.IsNullOrWhiteSpace(request.Model) ? _options.DefaultModel : request.Model;

            var requestJson = ClaudeRequestMapper.Map(
                model,
                messages,
                request.Temperature ?? _options.DefaultTemperature,
                request.MaxTokens ?? _options.DefaultMaxTokens);

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "messages")
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            };
            httpRequest.Headers.TryAddWithoutValidation("x-api-key", _options.ApiKey);
            httpRequest.Headers.TryAddWithoutValidation("anthropic-version", _options.ApiVersion);

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
                        Error.ProviderError($"Claude 调用失败: HTTP {response.StatusCode}, {raw}"));
                }

                var completion = _responseParser.Parse(raw);
                return ChatResponse.Success(completion.Content, completion.Usage);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return ChatResponse.Failure(Error.ProviderError($"Claude 请求超时: {ex.Message}"));
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
