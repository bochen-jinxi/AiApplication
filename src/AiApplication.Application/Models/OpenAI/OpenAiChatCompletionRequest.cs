using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Chat Completion 请求。
    /// </summary>
    public sealed class OpenAiChatCompletionRequest
    {
        /// <summary>
        /// 模型名称。
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// 聊天消息。
        /// </summary>
        [JsonPropertyName("messages")]
        public List<OpenAiChatMessage> Messages { get; set; }

        /// <summary>
        /// Temperature。
        /// </summary>
        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        /// <summary>
        /// 最大 Token 数。
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }

        /// <summary>
        /// TopP。
        /// </summary>
        [JsonPropertyName("top_p")]
        public float? TopP { get; set; }

        /// <summary>
        /// 是否开启流式输出。
        /// </summary>
        [JsonPropertyName("stream")]
        public bool Stream { get; set; }
      
    }
}