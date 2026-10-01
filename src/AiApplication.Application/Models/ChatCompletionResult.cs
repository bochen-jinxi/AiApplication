using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AiApplication.Application.Models
{
    public class ChatCompletionResult
    {
         /// <summary>
        /// 消息内容。
        /// </summary>
        public string Content { get; set; }
        public string FinishReason { get; set; }

        /// <summary>
        /// Prompt Token 数。
        /// </summary>        
        public int PromptTokens { get; set; }

        /// <summary>
        /// Completion Token 数。
        /// </summary>      
        public int CompletionTokens { get; set; }

        /// <summary>
        /// Total Token 数。
        /// </summary>        
        public int TotalTokens { get; set; }
    }
}
