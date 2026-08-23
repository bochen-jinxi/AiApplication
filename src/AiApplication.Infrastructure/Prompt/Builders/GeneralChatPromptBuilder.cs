using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Application.Chat;
using AiApplication.Application.Prompt;

namespace AiApplication.Infrastructure.Prompt.Builders
{
    /// <summary>
    /// 通用聊天提示词构建器。构建默认的系统提示词与用户提示词。
    /// </summary>
    public class GeneralChatPromptBuilder : IPromptBuilder
    {
        private const string DefaultSystemPrompt =
            "你是一个智能助手，请根据用户的问题给出准确、有帮助的回答。";

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
