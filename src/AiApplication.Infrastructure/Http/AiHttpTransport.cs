using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions;

namespace AiApplication.Infrastructure.Http
{
    /// <summary>
    /// AI HTTP 传输层。
    /// </summary>
    public sealed class AiHttpTransport : IAiHttpTransport
    {
        /// <summary>
        /// HttpClientFactory。
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 初始化 AI HTTP 传输层。
        /// </summary>
        /// <param name="httpClientFactory">
        /// IHttpClientFactory。
        /// </param>
        public AiHttpTransport(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// 发送 POST 请求。
        /// </summary>
        /// <param name="request">
        /// HTTP 请求。
        /// </param>
        /// <param name="cancellationToken">
        /// CancellationToken。
        /// </param>
        /// <returns>
        /// HttpResponseMessage。
        /// </returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            return response;
        }
    }
}