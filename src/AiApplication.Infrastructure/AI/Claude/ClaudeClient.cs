using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions.AI;
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

        public ClaudeClient(HttpClient httpClient, ClaudeOptions options, IAiResponseParser responseParser = null)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new System.ArgumentNullException(nameof(options));
            _responseParser = responseParser ?? new ClaudeResponseParser();
        }

        public async Task<Result<AiCompletion>> CompleteAsync(
            string model,
            IReadOnlyList<AiMessage> messages,
            double? temperature = null,
            int? maxTokens = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                model = _options.DefaultModel;
            }

            if (messages == null || messages.Count == 0)
            {
                return Result<AiCompletion>.Failure(Error.InvalidArgument("消息列表不能为空。"));
            }

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                return Result<AiCompletion>.Failure(Error.Unauthorized("Claude ApiKey 未配置。"));
            }

            var requestJson = ClaudeRequestMapper.Map(
                model,
                messages,
                temperature ?? _options.DefaultTemperature,
                maxTokens ?? _options.DefaultMaxTokens);

            using var request = new HttpRequestMessage(HttpMethod.Post, "messages")
            {
                Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("x-api-key", _options.ApiKey);
            request.Headers.TryAddWithoutValidation("anthropic-version", _options.ApiVersion);

            try
            {
                using var response = await _httpClient
                    .SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                var raw = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return Result<AiCompletion>.Failure(
                        Error.ProviderError($"Claude 调用失败: HTTP {response.StatusCode}, {raw}"));
                }

                var completion = _responseParser.Parse(raw);
                return Result<AiCompletion>.Success(completion);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return Result<AiCompletion>.Failure(Error.ProviderError($"Claude 请求超时: {ex.Message}"));
            }
        }
    }
}
