//using AiApplication.Application.Chat;
//using AiApplication.Application.Prompt;

//namespace AiApplication.Application.Abstractions.Prompt
//{
//    /// <summary>
//    /// 提示词构建器抽象。负责根据 <see cref="PromptRequest"/> 构建系统提示词与用户提示词。
//    /// </summary>
//    public interface IPromptBuilder
//    {
//        /// <summary>
//        /// 构建系统提示词消息。
//        /// </summary>
//        /// <param name="request">提示词请求，包含用户问题、RAG/MCP 上下文、语言等。</param>
//        /// <returns>系统提示词 <see cref="ChatMessage"/>。</returns>
//        ChatMessage BuildSystemPrompt(PromptRequest request);

//        /// <summary>
//        /// 构建用户提示词消息。
//        /// </summary>
//        /// <param name="request">提示词请求，包含用户问题、RAG/MCP 上下文、语言等。</param>
//        /// <returns>用户提示词 <see cref="ChatMessage"/>。</returns>
//        ChatMessage BuildUserPrompt(PromptRequest request);
//          PromptDocument Build(PromptRequest request);
//    }
//}
