using System.Text.Json.Serialization;

namespace AiApplication.Infrastructure.AI.OpenAI.Dtos
{
     /// <summary>
    /// OpenAI Token 使用统计。
    /// </summary>
    internal sealed class OpenAiUsage
    {
        /// <summary>
        /// Prompt Token 数。
        /// </summary>
        public int PromptTokens { get; set; }

        /// <summary>
        /// Completion Token 数。
        /// </summary>
        public int CompletionTokens { get; set; }
    }
}
