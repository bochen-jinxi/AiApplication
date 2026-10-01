using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.Abstractions
{
    /// <summary>
    /// AI HTTP 传输接口。
    /// </summary>
    public interface IAiHttpTransport
    {
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
        Task<string> PostAsync(
            string url,
            string json,
            string apiKey,
            CancellationToken cancellationToken);
    }
}