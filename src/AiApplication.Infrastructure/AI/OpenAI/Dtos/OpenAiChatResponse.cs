using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AiApplication.Infrastructure.AI.OpenAI.Dtos
{
   
    /// <summary>
    /// OpenAI 响应对象。
    /// </summary>
    internal sealed class OpenAiChatResponse
    {
        /// <summary>
        /// 模型名称。
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 返回选项。
        /// </summary>
        public List<OpenAiChoice> Choices { get; set; }

        /// <summary>
        /// 统计信息。
        /// </summary>
        public OpenAiUsage Usage { get; set; }
    }
}
