using System.Text.Json.Serialization;

namespace AiApplication.Infrastructure.AI.OpenAI.Models
{
    /// <summary>
    /// OpenAI Tool Call Function。
    /// </summary>
    public sealed class OpenAiToolCallFunction
    {
        /// <summary>
        /// Function 名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Function 参数。
        /// </summary>
        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }
    }
}