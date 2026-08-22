using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.Chat
{
    /// <summary>
    /// 聊天服务抽象。编排"提示词构建 → RAG 检索 → 工具调用 → AI 调用"的完整链路。
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// 处理一次聊天请求。
        /// </summary>
        Task<ChatResponse> ChatAsync(ChatRequest request, CancellationToken cancellationToken = default);
    }
}
