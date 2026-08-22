using System.Collections.Generic;
using AiApplication.Domain.AI;

namespace AiApplication.Application.Abstractions.Prompt
{
    /// <summary>
    /// 提示词构建器抽象。负责将用户输入、历史消息、RAG 上下文、工具结果等
    /// 组装成最终发送给 AI 的 <see cref="AiMessage"/> 列表。
    /// </summary>
    public interface IPromptBuilder
    {
        /// <summary>
        /// 构建完整的消息列表。
        /// </summary>
        /// <param name="systemPrompt">系统提示词。可为 null，由实现决定是否使用默认值。</param>
        /// <param name="history">历史对话消息。</param>
        /// <param name="userMessage">本次用户输入。</param>
        /// <param name="ragContexts">RAG 检索到的参考片段。可为空。</param>
        /// <param name="toolResults">工具调用结果。可为空。</param>
        /// <returns>最终发送给 AI 的消息列表。</returns>
        IReadOnlyList<AiMessage> Build(
            string systemPrompt,
            IReadOnlyList<AiMessage> history,
            string userMessage,
            IReadOnlyList<string> ragContexts = null,
            IReadOnlyList<string> toolResults = null);
    }
}
