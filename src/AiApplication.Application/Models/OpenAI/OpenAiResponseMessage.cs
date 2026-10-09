using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI 返回消息。
    /// </summary>
    public sealed class OpenAiResponseMessage
    {
        /// <summary>
        /// 消息角色。
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 消息内容。
        /// </summary>
        public string Content { get; set; }

         /// <summary>
         /// Tool Calls。
         /// </summary>
         [JsonPropertyName("tool_calls")]
         public List<OpenAiToolCall> ToolCalls { get; set; }
    }
}