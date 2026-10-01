//using System;
//using AiApplication.Domain.AI;
//using AiApplication.Domain.Common;

//namespace AiApplication.Application.Chat
//{
//    /// <summary>
//    /// 聊天响应。
//    ///
//    /// 职责：
//    /// 1. 承载一次 AI 调用的返回结果。
//    /// 2. 保存模型输出、Token 统计和错误信息。
//    /// 3. 作为 Application 层统一的响应模型。
//    ///
//    /// 说明：
//    /// 该对象与 ChatRequest 配套使用，
//    /// 最终由具体 AI Client 负责填充。
//    /// </summary>
//    public sealed class ChatResponse
//    {
//        /// <summary>
//        /// 回复内容。
//        /// </summary>
//        public string Content { get; private set; }

//        /// <summary>
//        /// AI 提供商。
//        /// </summary>
//        public AiProviderEnum Provider { get; private set; }

//        /// <summary>
//        /// 模型名称。
//        /// </summary>
//        public string Model { get; private set; }

//        /// <summary>
//        /// Prompt Token 数。
//        /// </summary>
//        public int PromptTokens { get; private set; }

//        /// <summary>
//        /// Completion Token 数。
//        /// </summary>
//        public int CompletionTokens { get; private set; }

//        /// <summary>
//        /// 总 Token 数。
//        /// </summary>
//        public int TotalTokens
//        {
//            get { return PromptTokens + CompletionTokens; }
//        }

//        /// <summary>
//        /// 是否成功。
//        /// </summary>
//        public bool IsSuccess { get; private set; }

//        /// <summary>
//        /// 错误信息。
//        /// </summary>
//        public string ErrorMessage { get; private set; }

//        /// <summary>
//        /// 结束原因。
//        /// 例如：stop、length、tool_calls。
//        /// </summary>
//        public string FinishReason { get; private set; }

//        /// <summary>
//        /// 响应时间（UTC）。
//        /// </summary>
//        public DateTime ResponseTimeUtc { get; private set; }

//        /// <summary>
//        /// Token 使用统计。
//        /// </summary>
//        public TokenUsage Usage { get; private set; }

//        /// <summary>
//        /// 错误对象。
//        /// </summary>
//        public Error Error { get; private set; }

//        /// <summary>
//        /// 创建成功响应。
//        /// </summary>
//        /// <param name="provider">AI 提供商。</param>
//        /// <param name="model">模型名称。</param>
//        /// <param name="content">回复内容。</param>
//        /// <param name="promptTokens">Prompt Token 数。</param>
//        /// <param name="completionTokens">Completion Token 数。</param>
//        /// <param name="finishReason">结束原因。</param>
//        /// <returns>成功的 ChatResponse。</returns>
//        public static ChatResponse Success(
//            AiProviderEnum provider,
//            string model,
//            string content,
//            int promptTokens,
//            int completionTokens,
//            string finishReason)
//        {
//            return new ChatResponse
//            {
//                Provider = provider,
//                Model = model,
//                Content = content,
//                PromptTokens = promptTokens,
//                CompletionTokens = completionTokens,
//                IsSuccess = true,
//                ErrorMessage = null,
//                FinishReason = finishReason,
//                ResponseTimeUtc = DateTime.UtcNow,
//                Usage = new TokenUsage(promptTokens, completionTokens),
//                Error = null
//            };
//        }

//        /// <summary>
//        /// 创建成功响应（基于内容和 Token 用量）。
//        /// </summary>
//        /// <param name="content">回复内容。</param>
//        /// <param name="usage">Token 用量。</param>
//        /// <returns>成功的 ChatResponse。</returns>
//        public static ChatResponse Success(string content, TokenUsage usage)
//        {
//            var safeUsage = usage ?? TokenUsage.Empty;
//            return new ChatResponse
//            {
//                Provider = AiProviderEnum.OpenAI,
//                Model = null,
//                Content = content,
//                PromptTokens = safeUsage.PromptTokens,
//                CompletionTokens = safeUsage.CompletionTokens,
//                IsSuccess = true,
//                ErrorMessage = null,
//                FinishReason = null,
//                ResponseTimeUtc = DateTime.UtcNow,
//                Usage = safeUsage,
//                Error = null
//            };
//        }

//        /// <summary>
//        /// 创建失败响应。
//        /// </summary>
//        /// <param name="provider">AI 提供商。</param>
//        /// <param name="model">模型名称。</param>
//        /// <param name="errorMessage">错误信息。</param>
//        /// <returns>失败的 ChatResponse。</returns>
//        public static ChatResponse Fail(
//            AiProviderEnum provider,
//            string model,
//            string errorMessage)
//        {
//            return new ChatResponse
//            {
//                Provider = provider,
//                Model = model,
//                Content = string.Empty,
//                PromptTokens = 0,
//                CompletionTokens = 0,
//                IsSuccess = false,
//                ErrorMessage = errorMessage,
//                FinishReason = null,
//                ResponseTimeUtc = DateTime.UtcNow,
//                Usage = TokenUsage.Empty,
//                Error = Error.Internal(errorMessage)
//            };
//        }

//        /// <summary>
//        /// 创建失败响应（基于 Error 对象）。
//        /// </summary>
//        /// <param name="error">错误对象。</param>
//        /// <returns>失败的 ChatResponse。</returns>
//        public static ChatResponse Failure(Error error)
//        {
//            var safeError = error ?? Error.Internal("未知错误。");
//            return new ChatResponse
//            {
//                Provider = AiProviderEnum.OpenAI,
//                Model = null,
//                Content = string.Empty,
//                PromptTokens = 0,
//                CompletionTokens = 0,
//                IsSuccess = false,
//                ErrorMessage = safeError.Message,
//                FinishReason = null,
//                ResponseTimeUtc = DateTime.UtcNow,
//                Usage = TokenUsage.Empty,
//                Error = safeError
//            };
//        }
//    }
//}
