using System.Text.Json.Serialization;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Token 使用统计。
    /// </summary>
    public sealed class OpenAiUsage
    {
        /// <summary>
        /// Prompt Token 数。
        /// </summary>
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        /// <summary>
        /// Completion Token 数。
        /// </summary>
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        /// <summary>
        /// Total Token 数。
        /// </summary>
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}