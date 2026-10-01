//using System.Collections.Generic;
//using System.Text.Json;
//using AiApplication.Domain.AI;

//namespace AiApplication.Infrastructure.AI.OpenAI
//{
//    /// <summary>
//    /// 将统一的 <see cref="AiMessage"/> 列表映射为 OpenAI Chat Completions API 的请求 JSON。
//    /// </summary>
//    public static class OpenAiRequestMapper
//    {
//        public static string Map(string model, IReadOnlyList<AiMessage> messages, double? temperature, int? maxTokens)
//        {
//            var body = new Dictionary<string, object>
//            {
//                ["model"] = model,
//                ["messages"] = MapMessages(messages)
//            };

//            if (temperature.HasValue)
//            {
//                body["temperature"] = temperature.Value;
//            }

//            if (maxTokens.HasValue)
//            {
//                body["max_tokens"] = maxTokens.Value;
//            }

//            return JsonSerializer.Serialize(body);
//        }

//        private static List<object> MapMessages(IReadOnlyList<AiMessage> messages)
//        {
//            var list = new List<object>(messages.Count);
//            foreach (var msg in messages)
//            {
//                list.Add(new Dictionary<string, string>
//                {
//                    ["role"] = MapRole(msg.Role),
//                    ["content"] = msg.Content
//                });
//            }

//            return list;
//        }

//        private static string MapRole(AiRoleEnum role)
//        {
//            switch (role)
//            {
//                case AiRoleEnum.System: return "system";
//                case AiRoleEnum.User: return "user";
//                case AiRoleEnum.Assistant: return "assistant";
//                case AiRoleEnum.Tool: return "tool";
//                default: return "user";
//            }
//        }
//    }
//}
