using System.Collections.Generic;

namespace AiApplication.Application.Models
{
    public class ChatCompletionRequest
    {
        /// <summary>
        /// 模型名称。
        /// </summary>
     
        public string Model { get; set; }

        public List<ChatMessage> Messages { get; set; }
        /// <summary>
        /// Temperature。
        /// </summary>

        public float? Temperature { get; set; }

        /// <summary>
        /// 最大 Token 数。
        /// </summary>
        
        public int? MaxTokens { get; set; }

        /// <summary>
        /// TopP。
        /// </summary>
        
        public float? TopP { get; set; }

        /// <summary>
        /// 是否开启流式输出。
        /// </summary>
        
        public bool Stream { get; set; }
    }
}
