using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions;

namespace AiApplication.Infrastructure.Http
{
    /// <summary>
    /// AI HTTP 传输实现。
    /// </summary>
    public sealed class AiHttpTransport : IAiHttpTransport
    {
        /// <summary>
        /// HttpClient 工厂。
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 初始化 HTTP 传输层。
        /// </summary>
        /// <param name="httpClientFactory">
        /// HttpClient 工厂。
        /// </param>
        public AiHttpTransport(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// 发送 POST 请求。
        /// </summary>
        /// <param name="url">
        /// 请求地址。
        /// </param>
        /// <param name="json">
        /// JSON 请求内容。
        /// </param>
        /// <param name="apiKey">
        /// API Key。
        /// </param>
        /// <param name="cancellationToken">
        /// 取消令牌。
        /// </param>
        /// <returns>
        /// HTTP 响应字符串。
        /// </returns>
        public async Task<string> PostAsync(
            string url,
            string json,
            string apiKey,
            CancellationToken cancellationToken)
        {
            EnsureNotEmpty(url, nameof(url));
            EnsureNotEmpty(json, nameof(json));
            EnsureNotEmpty(apiKey, nameof(apiKey));

            HttpClient httpClient = _httpClientFactory.CreateClient();

            using HttpRequestMessage request = CreatePostRequest(url, json, apiKey);

            using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 创建 POST 请求。
        /// </summary>
        /// <param name="url">
        /// 请求地址。
        /// </param>
        /// <param name="json">
        /// JSON 请求内容。
        /// </param>
        /// <param name="apiKey">
        /// API Key。
        /// </param>
        /// <returns>
        /// HttpRequestMessage。
        /// </returns>
        private static HttpRequestMessage CreatePostRequest(
            string url,
            string json,
            string apiKey)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return request;
        }

        /// <summary>
        /// 验证字符串不能为空。
        /// </summary>
        /// <param name="value">
        /// 字符串。
        /// </param>
        /// <param name="parameterName">
        /// 参数名称。
        /// </param>
        private static void EnsureNotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{parameterName} 不能为空。", parameterName);
            }
        }
    }
}