namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Streaming Delta。
    /// </summary>
    public sealed class OpenAiDelta
    {
        /// <summary>
        /// 角色。
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 增量内容。
        /// </summary>
        public string Content { get; set; }
    }
}