namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// Streaming Choice。
    /// </summary>
    public sealed class OpenAiChunkChoice
    {
        /// <summary>
        /// 增量内容。
        /// </summary>
        public OpenAiDelta Delta { get; set; }

        /// <summary>
        /// 完成原因。
        /// </summary>
        public string FinishReason { get; set; }

        /// <summary>
        /// 索引。
        /// </summary>
        public int Index { get; set; }
    }
}