using System.Text.Json.Serialization;

namespace AiApplication.Infrastructure.AI.OpenAI.Dtos
{

    /// <summary>
    /// OpenAI 响应选项。
    /// </summary>
    internal sealed class OpenAiChoice
    {
        /// <summary>
        /// 回复消息。
        /// </summary>
        public OpenAiMessage Message { get; set; }

        /// <summary>
        /// 结束原因。
        /// </summary>
        public string FinishReason { get; set; }
    }
}
