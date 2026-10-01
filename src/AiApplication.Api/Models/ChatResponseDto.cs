//using System;

//namespace AiApplication.Api.Models
//{
//    /// <summary>
//    /// 聊天响应 DTO。
//    ///
//    /// 职责：
//    /// 1. 将 Application 层返回结果映射为 API 输出。
//    /// 2. 隔离 API 层与 Application 层的数据结构。
//    /// </summary>
//    public sealed class ChatResponseDto
//    {
//        /// <summary>
//        /// 回复内容。
//        /// </summary>
//        public string Content { get; set; }

//        /// <summary>
//        /// AI 提供商。
//        /// </summary>
//        public string Provider { get; set; }

//        /// <summary>
//        /// 模型名称。
//        /// </summary>
//        public string Model { get; set; }

//        /// <summary>
//        /// Prompt Token 数。
//        /// </summary>
//        public int PromptTokens { get; set; }

//        /// <summary>
//        /// Completion Token 数。
//        /// </summary>
//        public int CompletionTokens { get; set; }

//        /// <summary>
//        /// 总 Token 数。
//        /// </summary>
//        public int TotalTokens { get; set; }

//        /// <summary>
//        /// 是否成功。
//        /// </summary>
//        public bool IsSuccess { get; set; }

//        /// <summary>
//        /// 错误信息。
//        /// </summary>
//        public string ErrorMessage { get; set; }

//        /// <summary>
//        /// 响应时间（UTC）。
//        /// </summary>
//        public DateTime ResponseTimeUtc { get; set; }
//    }
//}