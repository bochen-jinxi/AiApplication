namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// Streaming 返回的数据块。
    /// </summary>
    public sealed class StreamingChatChunk
    {
        /// <summary>
        /// 本次新增文本。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Role。
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// FinishReason。
        /// </summary>
        public string FinishReason { get; set; }

        /// <summary>
        /// 是否结束。
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}