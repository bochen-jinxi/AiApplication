//using System.Threading;
//using System.Threading.Tasks;

//namespace AiApplication.Application.Abstractions.AI
//{
//    /// <summary>
//    /// AI HTTP 传输接口。
//    ///
//    /// 职责：
//    /// 1. 负责发送 HTTP 请求。
//    /// 2. 屏蔽 HttpClient。
//    /// 3. 屏蔽网络实现细节。
//    /// 4. 不关心 OpenAI、Claude 等任何协议。
//    /// </summary>
//    public interface IAiHttpTransport
//    {
//        /// <summary>
//        /// 发送 HTTP 请求。
//        /// </summary>
//        /// <param name="request">
//        /// HTTP 请求对象。
//        /// </param>
//        /// <param name="cancellationToken">
//        /// 取消令牌。
//        /// </param>
//        /// <returns>
//        /// 返回服务器响应字符串。
//        /// </returns>
//        Task<string> SendAsync(
//            AiHttpRequest request,
//            CancellationToken cancellationToken);
//    }
//}