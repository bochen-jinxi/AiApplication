using System.Text.Json.Serialization;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI 返回结果。
    /// </summary>
    public sealed class OpenAiChoice
    {
        /// <summary>
        /// 返回序号。
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 消息。
        /// </summary>
        public OpenAiResponseMessage Message { get; set; }

        /// <summary>
        /// 结束原因。
        /// </summary>
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }
}