using System.Text.Json.Serialization;
namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Tool。
    /// </summary>
    public sealed class OpenAiTool
    {
        /// <summary>
        /// Tool 类型。
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Function。
        /// </summary>
        [JsonPropertyName("function")]
        public OpenAiFunction Function { get; set; }
    }
}