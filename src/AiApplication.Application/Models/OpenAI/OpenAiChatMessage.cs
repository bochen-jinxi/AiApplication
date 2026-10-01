using System.Text.Json.Serialization;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI 聊天消息。
    /// </summary>
    public sealed class OpenAiChatMessage
    {
        /// <summary>
        /// 消息角色。
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// 消息内容。
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}