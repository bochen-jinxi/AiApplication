//using System.Collections.Generic;
//using AiApplication.Domain.AI;

//namespace AiApplication.Application.Chat
//{
//    /// <summary>
//    /// 聊天请求。由 API 层构造，传入 <see cref="IChatService"/>。
//    /// </summary>
//    public class ChatRequest
//    {
//        /// <summary>
//        /// 用户输入的原始文本。
//        /// </summary>
//        public string UserMessage { get; set; }

//        /// <summary>
//        /// 目标 AI 提供商。默认 OpenAI。
//        /// </summary>
//        public AiProviderEnum Provider { get; set; }

//        /// <summary>
//        /// 目标模型名称。例如 "gpt-4o-mini"。
//        /// </summary>
//        public string Model { get; set; }

//        /// <summary>
//        /// 历史对话消息。不包含本次 <see cref="UserMessage"/>。
//        /// </summary>
//        public IReadOnlyList<AiMessage> History { get; set; }

//        /// <summary>
//        /// 系统提示词。可选，用于覆盖默认系统提示。
//        /// </summary>
//        public string SystemPrompt { get; set; }

//        /// <summary>
//        /// 采样温度。可选。
//        /// </summary>
//        public double? Temperature { get; set; }

//        /// <summary>
//        /// 最大生成 Token 数。可选。
//        /// </summary>
//        public int? MaxTokens { get; set; }

//        /// <summary>
//        /// 是否启用 RAG 检索增强。默认 false。
//        /// </summary>
//        public bool EnableRag { get; set; }

//        /// <summary>
//        /// 是否启用 MCP 工具调用。默认 false。
//        /// </summary>
//        public bool EnableTools { get; set; }

//        public ChatRequest()
//        {
//            Provider = AiProviderEnum.OpenAI;
//            History = System.Array.Empty<AiMessage>();
//        }

//        public ChatRequest(string userMessage, AiProviderEnum provider = AiProviderEnum.OpenAI, string model = null, IReadOnlyList<AiMessage> history = null, string systemPrompt = null, double? temperature = null, int? maxTokens = null, bool enableRag = false, bool enableTools = false)
//        {
//            UserMessage = userMessage ?? string.Empty;
//            Provider = provider;
//            Model = model;
//            History = history ?? System.Array.Empty<AiMessage>();
//            SystemPrompt = systemPrompt;
//            Temperature = temperature;
//            MaxTokens = maxTokens;
//            EnableRag = enableRag;
//            EnableTools = enableTools;
//        }
//    }
//}
