//using System.Collections.Generic;
//using System.Text.Json;
//using AiApplication.Domain.AI;

//namespace AiApplication.Infrastructure.AI.Claude
//{
//    /// <summary>
//    /// 将统一的 <see cref="AiMessage"/> 列表映射为 Anthropic Messages API 请求 JSON。
//    /// 注意：Claude 的 system 提示是顶层字段，不在 messages 数组中。
//    /// </summary>
//    public static class ClaudeRequestMapper
//    {
//        public static string Map(string model, IReadOnlyList<AiMessage> messages, double? temperature, int? maxTokens)
//        {
//            string systemPrompt = null;
//            var body = new Dictionary<string, object>
//            {
//                ["model"] = model
//            };

//            var mappedMessages = new List<object>(messages.Count);
//            foreach (var msg in messages)
//            {
//                if (msg.Role == AiRoleEnum.System)
//                {
//                    systemPrompt = msg.Content;
//                    continue;
//                }

//                mappedMessages.Add(new Dictionary<string, string>
//                {
//                    ["role"] = MapRole(msg.Role),
//                    ["content"] = msg.Content
//                });
//            }

//            if (systemPrompt != null)
//            {
//                body["system"] = systemPrompt;
//            }

//            body["messages"] = mappedMessages;
//            body["max_tokens"] = maxTokens ?? 1024;

//            if (temperature.HasValue)
//            {
//                body["temperature"] = temperature.Value;
//            }

//            return JsonSerializer.Serialize(body);
//        }

//        private static string MapRole(AiRoleEnum role)
//        {
//            switch (role)
//            {
//                case AiRoleEnum.User: return "user";
//                case AiRoleEnum.Assistant: return "assistant";
//                // Claude 不支持 tool 角色作为独立消息，简化处理为 user。
//                case AiRoleEnum.Tool: return "user";
//                default: return "user";
//            }
//        }
//    }
//}
