using System.Collections.Generic;
using System.Text.Json;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.AI.DeepSeek
{
    /// <summary>
    /// DeepSeek 请求映射器。DeepSeek 兼容 OpenAI Chat Completions 协议，
    /// 此处独立实现以保留未来扩展空间（如 reasoning_efficiency 等专属参数）。
    /// </summary>
    public static class DeepSeekRequestMapper
    {
        public static string Map(
            string model,
            IReadOnlyList<AiMessage> messages,
            double? temperature,
            int? maxTokens)
        {
            var body = new Dictionary<string, object>
            {
                ["model"] = model,
                ["messages"] = MapMessages(messages)
            };

            if (temperature.HasValue)
            {
                body["temperature"] = temperature.Value;
            }

            if (maxTokens.HasValue)
            {
                body["max_tokens"] = maxTokens.Value;
            }

            return JsonSerializer.Serialize(body);
        }

        private static List<object> MapMessages(IReadOnlyList<AiMessage> messages)
        {
            var list = new List<object>(messages.Count);
            foreach (var msg in messages)
            {
                list.Add(new Dictionary<string, string>
                {
                    ["role"] = MapRole(msg.Role),
                    ["content"] = msg.Content
                });
            }

            return list;
        }

        private static string MapRole(AiRole role)
        {
            switch (role)
            {
                case AiRole.System: return "system";
                case AiRole.User: return "user";
                case AiRole.Assistant: return "assistant";
                case AiRole.Tool: return "tool";
                default: return "user";
            }
        }
    }
}
