using AiApplication.Application.Tools.Schema;
using System.Text.Json.Serialization;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Function。
    /// </summary>
    public sealed class OpenAiFunction
    {
        /// <summary>
        /// Function 名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Function 描述。
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// 参数。
        /// </summary>
        [JsonPropertyName("parameters")]
        public OpenAiFunctionParameters Parameters { get; set; }
    }

    /// <summary>
    /// OpenAI Function Parameters。
    /// </summary>
    public sealed class OpenAiFunctionParameters : ToolSchema
    {
    }
}