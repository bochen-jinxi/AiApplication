using System.Text.Json;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.AI.DeepSeek
{
    /// <summary>
    /// 解析 DeepSeek 响应 JSON。结构与 OpenAI 兼容，复用相同解析逻辑。
    /// </summary>
    public sealed class DeepSeekResponseParser : IAiResponseParser
    {
        public AiCompletion Parse(string rawResponse)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
            {
                return new AiCompletion(string.Empty, TokenUsage.Empty, "empty");
            }

            using var doc = JsonDocument.Parse(rawResponse);
            var root = doc.RootElement;

            string content = string.Empty;
            string finishReason = null;

            if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
            {
                var firstChoice = choices[0];
                if (firstChoice.TryGetProperty("message", out var message) &&
                    message.TryGetProperty("content", out var contentEl) &&
                    contentEl.ValueKind == JsonValueKind.String)
                {
                    content = contentEl.GetString();
                }

                if (firstChoice.TryGetProperty("finish_reason", out var finishEl) &&
                    finishEl.ValueKind == JsonValueKind.String)
                {
                    finishReason = finishEl.GetString();
                }
            }

            TokenUsage usage = TokenUsage.Empty;
            if (root.TryGetProperty("usage", out var usageEl))
            {
                int prompt = usageEl.TryGetProperty("prompt_tokens", out var p) && p.ValueKind == JsonValueKind.Number
                    ? p.GetInt32()
                    : 0;
                int completion = usageEl.TryGetProperty("completion_tokens", out var c) && c.ValueKind == JsonValueKind.Number
                    ? c.GetInt32()
                    : 0;
                usage = new TokenUsage(prompt, completion);
            }

            return new AiCompletion(content, usage, finishReason);
        }
    }
}
