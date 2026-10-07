namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI 错误响应。
    /// </summary>
    public sealed class OpenAiErrorResponse
    {
        /// <summary>
        /// 错误信息。
        /// </summary>
        public OpenAiError Error { get; set; }
    }
}