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

        public async Task<ChatResponse> ChatAsync(ChatRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                return ChatResponse.Failure(Error.InvalidArgument("聊天请求不能为空。"));
            }

            if (string.IsNullOrWhiteSpace(request.UserMessage))
            {
                return ChatResponse.Failure(Error.InvalidArgument("用户消息不能为空。"));
            }

            var context = new ChatContext(request);

            // 1. RAG 检索
            if (request.EnableRag && _ragService != null)
            {
                var retrieved = await _ragService
                    .RetrieveAsync(request.UserMessage, cancellationToken)
                    .ConfigureAwait(false);
                if (retrieved != null)
                {
                    context.RetrievedContexts.AddRange(retrieved);
                }
            }

            // 2. 工具调用
            if (request.EnableTools && _mcpService != null)
            {
                var toolResults = await _mcpService
                    .ExecuteToolsAsync(request.UserMessage, cancellationToken)
                    .ConfigureAwait(false);
                if (toolResults != null)
                {
                    context.ToolResults.AddRange(toolResults);
                }
            }

            // 3. 提示词构建
            var messages = _promptBuilder.Build(
                request.SystemPrompt,
                request.History,
                request.UserMessage,
                context.RetrievedContexts,
                context.ToolResults);
            context.Messages.AddRange(messages);

            // 4. 路由到对应厂商客户端
            IAiClient client;
            try
            {
                client = _provider.GetClient(request.Provider);
            }
            catch (System.Exception ex)
            {
                return ChatResponse.Failure(Error.ProviderError($"无法获取 AI 客户端: {ex.Message}"));
            }

            // 5. 调用 AI
            var model = request.Model ?? GetDefaultModel(request.Provider);
            var result = await client
                .CompleteAsync(model, context.Messages, request.Temperature, request.MaxTokens, cancellationToken)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return ChatResponse.Failure(result.Error ?? Error.ProviderError("AI 调用失败。"));
            }

            return ChatResponse.Success(result.Value.Content, result.Value.Usage);
        }

        private static string GetDefaultModel(AiProvider provider)
        {
            switch (provider)
            {
                case AiProvider.OpenAI: return "gpt-4o-mini";
                case AiProvider.Claude: return "claude-3-5-sonnet-20240620";
                case AiProvider.DeepSeek: return "deepseek-chat";
                default: return "gpt-4o-mini";
            }
        }
    }
}
