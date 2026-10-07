namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI 错误信息。
    /// </summary>
    public sealed class OpenAiError
    {
        /// <summary>
        /// 错误消息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误类型。
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 参数。
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 错误代码。
        /// </summary>
        public string Code { get; set; }
    }
}