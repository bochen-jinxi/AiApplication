using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Application.Chat;
using AiApplication.Domain.AI;
using AiApplication.Domain.Common;
using AiApplication.Infrastructure.AI.OpenAI.Dtos;

namespace AiApplication.Infrastructure.AI.OpenAI
{
    public sealed class OpenAiClient : IAiClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAiOptions _options;
        private readonly JsonSerializerOptions _jsonOptions;

        public AiProvider Provider => AiProvider.OpenAI;

        public OpenAiClient(
            HttpClient httpClient,
            OpenAiOptions options,
            JsonSerializerOptions jsonOptions = null)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new System.ArgumentNullException(nameof(options));
            _jsonOptions = jsonOptions ?? new JsonSerializerOptions();
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
                return ChatResponse.Failure(Error.Unauthorized("OpenAI ApiKey 未配置。"));
            }

            var messages = BuildMessages(request);
            var model = string.IsNullOrWhiteSpace(request.Model) ? _options.DefaultModel : request.Model;

            var requestJson = OpenAiRequestMapper.Map(
                model,
                messages,
                request.Temperature ?? _options.DefaultTemperature,
                request.MaxTokens ?? _options.DefaultMaxTokens);

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "chat/completions");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
            httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            try
            {
                using var response = await _httpClient.SendAsync(
                    httpRequest,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken);

                var raw = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return ChatResponse.Failure(
                        Error.ProviderError($"OpenAI 调用失败: HTTP {response.StatusCode}, {raw}"));
                }

                var result = JsonSerializer.Deserialize<OpenAiChatResponse>(raw, _jsonOptions);
                if (result == null)
                {
                    return ChatResponse.Failure(Error.ProviderError("OpenAI 响应解析失败。"));
                }

                return OpenAiResponseMapper.Map(result);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return ChatResponse.Failure(Error.ProviderError($"OpenAI 请求超时: {ex.Message}"));
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
