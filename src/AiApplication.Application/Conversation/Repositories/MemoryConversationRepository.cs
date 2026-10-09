using System;
using System.Collections.Concurrent;

namespace AiApplication.Application.Conversation.Repositories
{
    /// <summary>
    /// 内存对话仓储。
    /// </summary>
    public sealed class MemoryConversationRepository
        : IConversationRepository
    {
        private readonly ConcurrentDictionary<Guid, ConversationSession> _sessions;

        public MemoryConversationRepository()
        {
            _sessions =
                new ConcurrentDictionary<Guid, ConversationSession>();
        }

        public ConversationSession Create()
        {
            ConversationSession session =
                new ConversationSession();

            _sessions.TryAdd(
                session.ConversationId,
                session);

            return session;
        }

        public ConversationSession Get(Guid conversationId)
        {
            _sessions.TryGetValue(
                conversationId,
                out ConversationSession session);

            return session;
        }

        public void Remove(Guid conversationId)
        {
            _sessions.TryRemove(
                conversationId,
                out _);
        }
    }
}