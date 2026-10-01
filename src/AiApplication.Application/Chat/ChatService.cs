//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using AiApplication.Application.Abstractions.AI;
//using Microsoft.Extensions.Logging;

//namespace AiApplication.Application.Chat
//{
//    /// <summary>
//    /// 聊天服务。
//    ///
//    /// 职责：
//    /// 1. 接收 API 层传入的 ChatRequest。
//    /// 2. 根据 Provider 选择对应的 AI Client。
//    /// 3. 将请求交给 AI Client 执行。
//    /// 4. 返回统一的 ChatResponse。
//    ///
//    /// 说明：
//    /// 该类只负责应用层编排，
//    /// 不直接依赖 OpenAI、Claude、DeepSeek 等具体实现。
//    /// </summary>
//    public sealed class ChatService : IChatService
//    {
//        private readonly IAiClientProvider _aiClientProvider;
//        private readonly ILogger<ChatService> _logger;

//        /// <summary>
//        /// 初始化聊天服务。
//        /// </summary>
//        /// <param name="aiClientProvider">AI 客户端提供者。</param>
//        /// <param name="logger">日志对象。</param>
//        public ChatService(IAiClientProvider aiClientProvider, ILogger<ChatService> logger)
//        {
//            _aiClientProvider = aiClientProvider ?? throw new ArgumentNullException(nameof(aiClientProvider));
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        }

//        /// <summary>
//        /// 执行聊天请求。
//        /// </summary>
//        /// <param name="request">聊天请求。</param>
//        /// <param name="cancellationToken">取消令牌。</param>
//        /// <returns>聊天响应。</returns>
//        public async Task<ChatResponse> ChatAsync(ChatRequest request, CancellationToken cancellationToken)
//        {
//            if (request == null)
//            {
//                throw new ArgumentNullException(nameof(request));
//            }          

//            _logger.LogInformation(
//                "开始处理聊天请求，Provider={0}, Model={1},  EnableRag={4}, EnableTools={5}",
//                request.Provider,
//                request.Model,
//                request.EnableRag,
//                request.EnableTools);

//            var client = _aiClientProvider.GetClient(request.Provider);

//            var response = await client.ChatAsync(request, cancellationToken);

//            _logger.LogInformation(
//                "聊天请求处理完成，Provider={0}, Success={1}, TotalTokens={2}",
//                response.Provider,
//                response.IsSuccess,
//                response.TotalTokens);

//            return response;
//        }
//    }
//}