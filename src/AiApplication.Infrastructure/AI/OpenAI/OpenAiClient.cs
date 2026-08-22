using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Domain.AI;
using AiApplication.Domain.Common;

namespace AiApplication.Infrastructure.AI.OpenAI
{
    /// <summary>
    /// OpenAI Chat Completions 客户端实现。
    /// 通过 <see cref="HttpClient"/> 调用 https://api.openai.com/v1/chat/completions。
    /// </summary>
    public sealed class OpenAiClient : IAiClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAiOptions _options;
        private readonly IAiResponseParser _responseParser;

        public AiProvider Provider => AiProvider.OpenAI;

        public OpenAiClient(HttpClient httpClient, OpenAiOptions options, IAiResponseParser responseParser = null)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new System.ArgumentNullException(nameof(options));
            _responseParser = responseParser ?? new OpenAiResponseParser();
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
                return Result<AiCompletion>.Failure(Error.Unauthorized("OpenAI ApiKey 未配置。"));
            }

            var requestJson = OpenAiRequestMapper.Map(
                model,
                messages,
                temperature ?? _options.DefaultTemperature,
                maxTokens ?? _options.DefaultMaxTokens);

            using var request = new HttpRequestMessage(HttpMethod.Post, "chat/completions")
            {
                Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);

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
                        Error.ProviderError($"OpenAI 调用失败: HTTP {response.StatusCode}, {raw}"));
                }

                var completion = _responseParser.Parse(raw);
                return Result<AiCompletion>.Success(completion);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return Result<AiCompletion>.Failure(Error.ProviderError($"OpenAI 请求超时: {ex.Message}"));
            }
        }
    }
}
