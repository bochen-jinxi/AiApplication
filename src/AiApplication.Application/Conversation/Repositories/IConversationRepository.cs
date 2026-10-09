using System;

namespace AiApplication.Application.Conversation.Repositories
{
    /// <summary>
    /// 对话仓储。
    /// </summary>
    public interface IConversationRepository
    {
        ConversationSession Create();

        ConversationSession Get(Guid conversationId);

        void Remove(Guid conversationId);
    }
}