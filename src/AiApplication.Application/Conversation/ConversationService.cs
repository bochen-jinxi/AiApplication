using System;
using System.Collections.Concurrent;
using AiApplication.Application.Conversation.Repositories;

namespace AiApplication.Application.Conversation
{
    /// <summary>
    /// 对话服务。
    /// </summary>
    public sealed class ConversationService
        : IConversationService
    {
        private readonly IConversationRepository _repository;
        public ConversationService(
    IConversationRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 创建会话。
        /// </summary>
        public ConversationSession Create()
{
    return _repository.Create();
}

        /// <summary>
        /// 获取会话。
        /// </summary>
        public ConversationSession Get(
            Guid conversationId)
        {
             
            return   _repository.Get(conversationId);
        }

        /// <summary>
        /// 删除会话。
        /// </summary>
        public void Remove(
            Guid conversationId)
        {
                  _repository.Remove(conversationId);
        }
    }
}