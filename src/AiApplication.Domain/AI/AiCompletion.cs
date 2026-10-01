//namespace AiApplication.Domain.AI
//{
//    /// <summary>
//    /// AI 调用结果。封装一次 AI 推理的回复内容、Token 用量与停止原因。
//    /// </summary>
//    public sealed class AiCompletion
//    {
//        /// <summary>
//        /// 回复文本内容。
//        /// </summary>
//        public string Content { get; }

//        /// <summary>
//        /// Token 使用统计。
//        /// </summary>
//        public TokenUsage Usage { get; }

//        /// <summary>
//        /// 停止原因。例如 "stop"、"length"、"content_filter" 等。
//        /// </summary>
//        public string FinishReason { get; }

//        public AiCompletion(string content, TokenUsage usage, string finishReason)
//        {
//            Content = content ?? string.Empty;
//            Usage = usage ?? TokenUsage.Empty;
//            FinishReason = finishReason;
//        }
//    }
//}
