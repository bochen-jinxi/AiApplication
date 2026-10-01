//using System.Text.Json;
//using AiApplication.Application.Abstractions.AI;
//using AiApplication.Domain.AI;

//namespace AiApplication.Infrastructure.AI.Claude
//{
//    /// <summary>
//    /// 解析 Anthropic Messages API 响应 JSON 为 <see cref="AiCompletion"/>。
//    /// Claude 响应结构：content 是数组，元素含 type=text 与 text 字段。
//    /// </summary>
//    public sealed class ClaudeResponseParser : IAiResponseParser
//    {
//        public AiCompletion Parse(string rawResponse)
//        {
//            if (string.IsNullOrWhiteSpace(rawResponse))
//            {
//                return new AiCompletion(string.Empty, TokenUsage.Empty, "empty");
//            }

//            using var doc = JsonDocument.Parse(rawResponse);
//            var root = doc.RootElement;

//            string content = string.Empty;
//            if (root.TryGetProperty("content", out var contentArr) && contentArr.ValueKind == JsonValueKind.Array)
//            {
//                var sb = new System.Text.StringBuilder();
//                foreach (var block in contentArr.EnumerateArray())
//                {
//                    if (block.TryGetProperty("type", out var typeEl) &&
//                        typeEl.ValueKind == JsonValueKind.String &&
//                        typeEl.GetString() == "text" &&
//                        block.TryGetProperty("text", out var textEl) &&
//                        textEl.ValueKind == JsonValueKind.String)
//                    {
//                        sb.Append(textEl.GetString());
//                    }
//                }

//                content = sb.ToString();
//            }

//            string stopReason = null;
//            if (root.TryGetProperty("stop_reason", out var stopEl) && stopEl.ValueKind == JsonValueKind.String)
//            {
//                stopReason = stopEl.GetString();
//            }

//            TokenUsage usage = TokenUsage.Empty;
//            if (root.TryGetProperty("usage", out var usageEl))
//            {
//                int prompt = usageEl.TryGetProperty("input_tokens", out var p) && p.ValueKind == JsonValueKind.Number
//                    ? p.GetInt32()
//                    : 0;
//                int completion = usageEl.TryGetProperty("output_tokens", out var c) && c.ValueKind == JsonValueKind.Number
//                    ? c.GetInt32()
//                    : 0;
//                usage = new TokenUsage(prompt, completion);
//            }

//            return new AiCompletion(content, usage, stopReason);
//        }
//    }
//}
