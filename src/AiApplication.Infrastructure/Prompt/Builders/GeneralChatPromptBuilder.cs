using System.Collections.Generic;
using System.Text;
using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.Prompt.Builders
{
    /// <summary>
    /// 通用聊天提示词构建器。组装顺序：system → history → user（含 RAG 上下文与工具结果）。
    /// </summary>
    public class GeneralChatPromptBuilder : IPromptBuilder
    {
        private const string DefaultSystemPrompt =
            "你是一个有帮助的 AI 助手。请根据用户的问题给出准确、简洁的回答。";

        public IReadOnlyList<AiMessage> Build(
            string systemPrompt,
            IReadOnlyList<AiMessage> history,
            string userMessage,
            IReadOnlyList<string> ragContexts = null,
            IReadOnlyList<string> toolResults = null)
        {
            var messages = new List<AiMessage>();

            // 1. System
            messages.Add(AiMessage.System(systemPrompt ?? DefaultSystemPrompt));

            // 2. History
            if (history != null)
            {
                messages.AddRange(history);
            }

            // 3. User（拼接 RAG 上下文与工具结果）
            var userContent = BuildUserContent(userMessage, ragContexts, toolResults);
            messages.Add(AiMessage.User(userContent));

            return messages;
        }

        private static string BuildUserContent(
            string userMessage,
            IReadOnlyList<string> ragContexts,
            IReadOnlyList<string> toolResults)
        {
            var sb = new StringBuilder();

            if (ragContexts != null && ragContexts.Count > 0)
            {
                sb.AppendLine("以下是检索到的相关参考资料：");
                for (int i = 0; i < ragContexts.Count; i++)
                {
                    sb.AppendLine($"[{i + 1}] {ragContexts[i]}");
                }

                sb.AppendLine();
                sb.AppendLine("请结合以上资料回答用户问题。");
                sb.AppendLine();
            }

            if (toolResults != null && toolResults.Count > 0)
            {
                sb.AppendLine("以下是工具调用结果：");
                for (int i = 0; i < toolResults.Count; i++)
                {
                    sb.AppendLine($"[Tool {i + 1}] {toolResults[i]}");
                }

                sb.AppendLine();
            }

            sb.Append(userMessage);
            return sb.ToString();
        }
    }
}
