using System.Collections.Generic;

namespace AiApplication.Infrastructure.AI.OpenAI.Dtos
{
   /// <summary>
    /// OpenAI 请求对象。
    /// </summary>
    internal sealed class OpenAiChatRequest
    {
        /// <summary>
        /// 模型名称。
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 消息列表。
        /// </summary>
        public List<OpenAiMessage> Messages { get; set; }

        /// <summary>
        /// 采样温度。
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// 最大 Token 数。
        /// </summary>
        public int MaxTokens { get; set; }

        /// <summary>
        /// 是否流式输出。
        /// </summary>
        public bool Stream { get; set; }
    }


}

