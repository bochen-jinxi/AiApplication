using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Application.MCP;
using AiApplication.Application.RAG;
using AiApplication.Domain.AI;
using AiApplication.Domain.Common;

namespace AiApplication.Application.Chat
{
    /// <summary>
    /// 聊天服务实现。编排完整的对话流程：
    /// 1. RAG 检索（可选）—— 根据用户输入检索知识片段；
    /// 2. 工具调用（可选）—— 执行 MCP 工具获取外部数据；
    /// 3. 提示词构建 —— 将 system / history / user / rag / tool 组装为消息列表；
    /// 4. AI 调用 —— 通过 <see cref="IAiClientProvider"/> 路由到具体厂商客户端；
    /// 5. 结果封装 —— 返回统一的 <see cref="ChatResponse"/>。
    /// </summary>
    public sealed class ChatService : IChatService
    {
        private readonly IAiClientProvider _provider;
        private readonly IPromptBuilder _promptBuilder;
        private readonly IRagService _ragService;
        private readonly IMcpService _mcpService;

        public ChatService(
            IAiClientProvider provider,
            IPromptBuilder promptBuilder,
            IRagService ragService = null,
            IMcpService mcpService = null)
        {
            _provider = provider ?? throw new System.ArgumentNullException(nameof(provider));
            _promptBuilder = promptBuilder ?? throw new System.ArgumentNullException(nameof(promptBuilder));
            _ragService = ragService;
            _mcpService = mcpService;
        }

           public async Task<ChatResponse> ChatAsync(
        ChatRequest request,
        CancellationToken cancellationToken)
        {


            var client =
                _provider.GetClient(
                    request.Provider);



            return await client.ChatAsync(
                request,
                cancellationToken);

        }
 
    }
}
