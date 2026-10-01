//using System;
//using System.Collections.Generic;
//using AiApplication.Domain.AI;

//namespace AiApplication.Api.Models
//{
//    /// <summary>
//    /// 聊天请求 DTO。
//    ///
//    /// 职责：
//    /// 1. 接收前端传入的 JSON。
//    /// 2. 作为 API 层输入模型。
//    /// 3. 转换为 Application 层的 ChatRequest。
//    /// </summary>
//    public sealed class ChatRequestDto
//    {
//        /// <summary>
//        /// 用户输入的原始文本。
//        /// </summary>
//        public string UserMessage { get; set; }

//        /// <summary>
//        /// 目标 AI 提供商。
//        /// </summary>
//        public AiProviderEnum? Provider { get; set; }

//        /// <summary>
//        /// 目标模型名称。
//        /// </summary>
//        public string Model { get; set; }

//        /// <summary>
//        /// 历史对话消息。
//        /// </summary>
//        public IReadOnlyList<AiMessage> History { get; set; }

//        /// <summary>
//        /// 系统提示词。
//        /// </summary>
//        public string SystemPrompt { get; set; }

//        /// <summary>
//        /// 温度参数。
//        /// </summary>
//        public double? Temperature { get; set; }

//        /// <summary>
//        /// 最大 Token 数。
//        /// </summary>
//        public int? MaxTokens { get; set; }

//        /// <summary>
//        /// 是否启用 RAG。
//        /// </summary>
//        public bool EnableRag { get; set; }

//        /// <summary>
//        /// 是否启用工具调用。
//        /// </summary>
//        public bool EnableTools { get; set; }
//    }
//}