using System.Collections.Generic;

namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Streaming Chunk。
    /// </summary>
    public sealed class OpenAiChatCompletionChunk
    {
        /// <summary>
        /// Id。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Object。
        /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public long Created { get; set; }

        /// <summary>
        /// 模型。
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Choices。
        /// </summary>
        public List<OpenAiChunkChoice> Choices { get; set; }
    }
}