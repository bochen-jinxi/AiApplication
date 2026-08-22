namespace AiApplication.Domain.AI
{
    /// <summary>
    /// 一次 AI 调用的 Token 使用统计。
    /// </summary>
    public sealed class TokenUsage
    {
        public int PromptTokens { get; }

        public int CompletionTokens { get; }

        public int TotalTokens => PromptTokens + CompletionTokens;

        public TokenUsage(int promptTokens, int completionTokens)
        {
            if (promptTokens < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(promptTokens), "PromptTokens 不能为负数。");
            }

            if (completionTokens < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(completionTokens), "CompletionTokens 不能为负数。");
            }

            PromptTokens = promptTokens;
            CompletionTokens = completionTokens;
        }

        public static TokenUsage Empty => new TokenUsage(0, 0);
    }
}
