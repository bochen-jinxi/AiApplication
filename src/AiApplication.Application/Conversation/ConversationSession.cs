using System;
using System.Collections.Generic;
using System.Text;

namespace AiApplication.Application.Conversation
{
    /// <summary>
    /// 会话。
    /// </summary>
    public sealed class ConversationSession
    {
        /// <summary>
        /// 会话 Id。
        /// </summary>
        public Guid ConversationId { get; }

        /// <summary>
        /// 消息。
        /// </summary>
        public IList<ConversationMessage> Messages { get; }

        /// <summary>
        /// Assistant 输出缓存。
        /// </summary>
        public StringBuilder AssistantBuffer { get; }

        public ConversationSession()
        {
            ConversationId = Guid.NewGuid();

            Messages =
                new List<ConversationMessage>();

            AssistantBuffer =
                new StringBuilder();
        }
    }
}