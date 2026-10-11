using AiApplication.Infrastructure.AI.OpenAI.Models;
using System.Text.Json.Serialization;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Tool Call。
    /// </summary>
    public sealed class OpenAiToolCall
    {
        /// <summary>
        /// 调用 Id。
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// 类型。
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Function。
        /// </summary>
        [JsonPropertyName("function")]
        public OpenAiToolCallFunction Function { get; set; }
    }
}



