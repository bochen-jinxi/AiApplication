using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.Abstractions
{
    /// <summary>
    /// AI HTTP 传输层。
    /// </summary>
    public interface IAiHttpTransport
    {
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
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}