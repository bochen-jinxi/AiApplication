using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Application.Chat;
using AiApplication.Application.Prompt;

namespace AiApplication.Infrastructure.Prompt.Builders
{
    /// <summary>
    /// 合同领域专用提示词构建器。
    /// </summary>
    public class ContractPromptBuilder : IPromptBuilder
    {
        public ChatMessage BuildSystemPrompt(PromptRequest request)
        {
            return new ChatMessage();
        }

        public ChatMessage BuildUserPrompt(PromptRequest request)
        {
            return new ChatMessage();
        }

        public PromptDocument Build(PromptRequest request)
        {
            return new PromptDocument();
        }
    }
}
