using System.Text.Json.Serialization;

namespace AiApplication.Infrastructure.AI.OpenAI.Dtos
{
     /// <summary>
    /// OpenAI 消息对象。
    /// </summary>
    internal sealed class OpenAiMessage
    {
        /// <summary>
        /// 角色。
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content { get; set; }
    }
}
