using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Chat;
using AiApplication.Domain.AI;

namespace AiApplication.Application.Abstractions.AI
{
    /// <summary>
    /// AI 客户端抽象。屏蔽不同 AI 提供商（OpenAI / Claude / DeepSeek）的 HTTP 协议差异，
    /// 对上层只暴露统一的"对话"语义。
    /// </summary>
    public interface IAiClient
    {
          AiProvider Provider
    {
        get;
    }

        /// <summary>
        /// 发起一次对话请求。
        /// </summary>
        /// <param name="request">对话请求，包含用户消息、提供商、模型、历史、采样参数等。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>对话响应，封装回复内容、Token 用量或错误。</returns>
        Task<ChatResponse> ChatAsync(
            ChatRequest request,
            CancellationToken cancellationToken);
    }
}
