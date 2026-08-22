using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Domain.AI;
using AiApplication.Domain.Common;

namespace AiApplication.Application.Abstractions.AI
{
    /// <summary>
    /// AI 客户端抽象。屏蔽不同 AI 提供商（OpenAI / Claude / DeepSeek）的 HTTP 协议差异，
    /// 对上层只暴露统一的"对话补全"语义。
    /// </summary>
    public interface IAiClient
    {
        /// <summary>
        /// 当前客户端绑定的提供商。
        /// </summary>
        AiProvider Provider { get; }

        /// <summary>
        /// 发起一次对话补全请求。
        /// </summary>
        /// <param name="model">目标模型名称，例如 "gpt-4o-mini" / "claude-3-5-sonnet" / "deepseek-chat"。</param>
        /// <param name="messages">完整对话消息列表（含 system / user / assistant / tool）。</param>
        /// <param name="temperature">采样温度，0.0 ~ 2.0，默认由各提供商决定。</param>
        /// <param name="maxTokens">最大生成 Token 数，传 null 表示使用模型默认值。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>成功时返回助手回复内容与 Token 用量；失败时返回 <see cref="Error"/>。</returns>
        Task<Result<AiCompletion>> CompleteAsync(
            string model,
            IReadOnlyList<AiMessage> messages,
            double? temperature = null,
            int? maxTokens = null,
            CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// AI 对话补全结果。
    /// </summary>
    public sealed class AiCompletion
    {
        public string Content { get; }

        public TokenUsage Usage { get; }

        public string FinishReason { get; }

        public AiCompletion(string content, TokenUsage usage, string finishReason = null)
        {
            Content = content ?? string.Empty;
            Usage = usage ?? TokenUsage.Empty;
            FinishReason = finishReason;
        }
    }
}
