//using System.Threading;
//using System.Threading.Tasks;

//namespace AiApplication.Application.Chat
//{
//    /// <summary>
//    /// 聊天服务抽象。
//    ///
//    /// 职责：
//    /// 1. 接收 API 层传入的 ChatRequest。
//    /// 2. 编排 RAG、MCP、Prompt、AI Client 等流程。
//    /// 3. 返回统一 ChatResponse。
//    /// </summary>
//    public interface IChatService
//    {
//        /// <summary>
//        /// 执行聊天请求。
//        /// </summary>
//        /// <param name="request">
//        /// 聊天请求。
//        /// </param>
//        /// <param name="cancellationToken">
//        /// 取消令牌。
//        /// </param>
//        /// <returns>
//        /// 聊天响应。
//        /// </returns>
//        Task<ChatResponse> ChatAsync(ChatRequest request, CancellationToken cancellationToken);
//    }
//}