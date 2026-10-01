using AiApplication.Application.Enums;

namespace AiApplication.Application.Models
{
    /// <summary>
    /// 聊天消息。
    /// </summary>
    public sealed class ChatMessage
    {
        /// <summary>
        /// 消息角色。
        /// </summary>
        public MessageRole Role { get; set; }

        /// <summary>
        /// 消息内容。
        /// </summary>
        public string Content { get; set; }
    }
}