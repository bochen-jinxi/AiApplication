namespace AiApplication.Domain.AI
{
    /// <summary>
    /// AI 服务提供商标识。用于在 <see cref="Abstractions.AI.IAiClientProvider"/> 中路由到具体的客户端实现。
    /// </summary>
    public enum AiProvider
    {
        OpenAI = 0,
        Claude = 1,
        DeepSeek = 2
    }
}
