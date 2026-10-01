//using AiApplication.Application.Chat;
//using AiApplication.Domain.AI;
//using AiApplication.Domain.Common;
//using AiApplication.Infrastructure.AI.OpenAI.Dtos;

//namespace AiApplication.Infrastructure.AI.OpenAI
//{
//    /// <summary>
//    /// 将 OpenAI Chat Completions 响应 DTO 映射为统一的 <see cref="ChatResponse"/>。
//    /// </summary>
//    internal static class OpenAiResponseMapper
//    {
//        public static ChatResponse Map(OpenAiChatResponse response)
//        {
//            if (response == null)
//            {
//                return ChatResponse.Failure(Error.ProviderError("OpenAI 响应为空。"));
//            }

//            if (response.Choices == null || response.Choices.Count == 0)
//            {
//                return ChatResponse.Failure(Error.ProviderError("OpenAI 没有返回任何回答。"));
//            }

//            var choice = response.Choices[0];
//            var content = choice.Message?.Content ?? string.Empty;

//            TokenUsage usage = TokenUsage.Empty;
//            if (response.Usage != null)
//            {
//                usage = new TokenUsage(response.Usage.PromptTokens, response.Usage.CompletionTokens);
//            }

//            return ChatResponse.Success(content, usage);
//        }
//    }
//}
