//using System.Collections.Generic;
//using AiApplication.Domain.AI;

//namespace AiApplication.Application.Chat
//{
//    /// <summary>
//    /// 聊天上下文。在一次 ChatService 调用过程中聚合所有中间数据，
//    /// 例如 RAG 检索结果、工具调用结果、最终发送给 AI 的消息列表等。
//    /// </summary>
//    public sealed class ChatContext
//    {
//        public ChatRequest Request { get; }

//        /// <summary>
//        /// 最终发送给 AI 的消息列表（含 system / history / user）。
//        /// </summary>
//        public List<AiMessage> Messages { get; } = new List<AiMessage>();

//        /// <summary>
//        /// RAG 检索到的参考文档片段。若未启用 RAG 则为空。
//        /// </summary>
//        public List<string> RetrievedContexts { get; } = new List<string>();

//        /// <summary>
//        /// 工具调用产生的中间结果。若未启用工具则为空。
//        /// </summary>
//        public List<string> ToolResults { get; } = new List<string>();

//        public ChatContext(ChatRequest request)
//        {
//            Request = request ?? new ChatRequest(string.Empty);
//        }
//    }
//}
