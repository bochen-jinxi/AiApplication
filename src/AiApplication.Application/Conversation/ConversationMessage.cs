using System;

namespace AiApplication.Application.Conversation
{
    /// <summary>
    /// 对话消息。
    /// </summary>
    public sealed class ConversationMessage
    {
        /// <summary>
        /// Role。
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}