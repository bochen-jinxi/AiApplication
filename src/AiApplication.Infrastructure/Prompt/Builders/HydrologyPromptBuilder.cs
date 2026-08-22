using System.Collections.Generic;
using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.Prompt.Builders
{
    /// <summary>
    /// 水文领域专用提示词构建器。在通用构建器基础上注入水文领域系统提示。
    /// </summary>
    public class HydrologyPromptBuilder : IPromptBuilder
    {
        private const string HydrologySystemPrompt =
            "你是一名水文领域专家助手。请基于水文专业知识回答问题，" +
            "涉及水文计算、水资源评价、水文预报等内容时请给出专业、严谨的解答。";

        private readonly GeneralChatPromptBuilder _inner = new GeneralChatPromptBuilder();

        public IReadOnlyList<AiMessage> Build(
            string systemPrompt,
            IReadOnlyList<AiMessage> history,
            string userMessage,
            IReadOnlyList<string> ragContexts = null,
            IReadOnlyList<string> toolResults = null)
        {
            // 若调用方未显式指定 systemPrompt，则使用水文领域默认提示。
            return _inner.Build(
                systemPrompt ?? HydrologySystemPrompt,
                history,
                userMessage,
                ragContexts,
                toolResults);
        }
    }
}
