using System;

namespace AiApplication.Application.Conversation
{
    /// <summary>
    /// 对话服务。
    /// </summary>
    public interface IConversationService
    {
        /// <summary>
        /// 创建会话。
        /// </summary>
        ConversationSession Create();

        /// <summary>
        /// 获取会话。
        /// </summary>
        ConversationSession Get(Guid conversationId);

        /// <summary>
        /// 删除会话。
        /// </summary>
        void Remove(Guid conversationId);
    }
}