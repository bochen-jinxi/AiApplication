using System.Collections.Generic;
using AiApplication.Application.Enums;
using AiApplication.Application.Models.OpenAI;

namespace AiApplication.Application.Models
{
    /// <summary>
    /// 聊天消息。
    /// 统一表示 system / user / assistant / tool 四种消息。
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

        /// <summary>
        /// Tool 消息对应的 ToolCallId。
        /// 仅 Tool Message 使用。
        /// </summary>
        public string ToolCallId { get; set; }

        /// <summary>
        /// Assistant 返回的 ToolCalls。
        /// 仅 Assistant Message 使用。
        /// </summary>
        public IReadOnlyList<OpenAiToolCall> ToolCalls { get; set; }
    }
}