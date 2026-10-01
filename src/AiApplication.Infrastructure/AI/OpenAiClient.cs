using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions;
using AiApplication.Application.Enums;
using AiApplication.Application.Models;
using AiApplication.Application.Models.OpenAI;
using AiApplication.Application.Options;
using Microsoft.Extensions.Options;

namespace AiApplication.Infrastructure.AI
{
    /// <summary>
    /// OpenAI Client。
    /// </summary>
    public sealed class OpenAiClient : IAiClient
    {
        /// <summary>
        /// OpenAI 配置。
        /// </summary>
        private readonly AiProviderOptions _providerOptions;

        /// <summary>
        /// HTTP 传输层。
        /// </summary>
        private readonly IAiHttpTransport _httpTransport;

        /// <summary>
        /// JSON 序列化器。
        /// </summary>
        private readonly IAiSerializer _serializer;

        /// <summary>
        /// 初始化 OpenAI Client。
        /// </summary>
        public OpenAiClient(
            IOptions<AiOptions> options,
            IAiHttpTransport httpTransport,
            IAiSerializer serializer)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            AiOptions aiOptions = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (!aiOptions.Providers.TryGetValue(AiProvider.OpenAI, out AiProviderOptions providerOptions))
            {
                throw new InvalidOperationException("未找到 OpenAI Provider 配置。");
            }

            ValidateProviderOptions(providerOptions);

            _providerOptions = providerOptions;
            _httpTransport = httpTransport ?? throw new ArgumentNullException(nameof(httpTransport));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <summary>
        /// 聊天补全。
        /// </summary>
        public async Task<ChatCompletionResult> ChatAsync(
     ChatCompletionRequest request,
     CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            OpenAiChatCompletionRequest openAiRequest = BuildRequest(request);

            string requestJson = _serializer.Serialize(openAiRequest);
            /*
             {"id":"chatcmpl-da76ee9d31ed4867b83ad0ef",
            "object":"chat.completion",
            "created":1790847129,
            "model":"gpt-5.6-luna",
            "choices":[{"index":0,"message":{"role":"assistant","content":"你好！我是 ChatGPT，由 OpenAI 训练的人工智能助手。\n\n我可以帮助你：\n- 解答问题、讲解知识\n- 撰写和修改文章、邮件、报告\n- 翻译、总结和提炼信息\n- 协助编程、调试代码\n- 制定学习、工作或旅行计划\n- 进行头脑风暴和日常对话\n\n我会尽力提供准确、清晰、实用的回答；如果信息可能过时或需要核实，我也会提醒你。很高兴认识你！"},"finish_reason":"stop"}],
            "usage":{"prompt_tokens":23,"completion_tokens":126,"total_tokens":149,"prompt_tokens_details":{"cached_tokens":0},"completion_tokens_details":{"reasoning_tokens":0}}}
             */
            string responseJson = await _httpTransport.PostAsync(
                _providerOptions.BaseUrl,
                requestJson,
                _providerOptions.ApiKey,
                cancellationToken).ConfigureAwait(false);

            OpenAiChatCompletionResponse response =
    _serializer.Deserialize<OpenAiChatCompletionResponse>(responseJson);

            if (response == null)
            {
                throw new InvalidOperationException("OpenAI 返回为空。");
            }

            if (response.Choices == null || response.Choices.Count == 0)
            {
                throw new InvalidOperationException("OpenAI 未返回任何 Choice。");
            }

            OpenAiChoice choice = response.Choices[0];

            if (choice.Message == null)
            {
                throw new InvalidOperationException("OpenAI 返回 Message 为空。");
            }

            ChatCompletionResult result = new ChatCompletionResult
            {
                Content = choice.Message.Content,
                FinishReason = choice.FinishReason,
                PromptTokens = response.Usage?.PromptTokens ?? 0,
                CompletionTokens = response.Usage?.CompletionTokens ?? 0,
                TotalTokens = response.Usage?.TotalTokens ?? 0
            };

            return result;
        }

        /// <summary>
        /// 构建 OpenAI 请求对象。
        /// </summary>
        private OpenAiChatCompletionRequest BuildRequest(ChatCompletionRequest request)
        {
            OpenAiChatCompletionRequest openAiRequest = new OpenAiChatCompletionRequest
            {
                Model = _providerOptions.Model,
                Temperature = request.Temperature,
                TopP = request.TopP,
                MaxTokens = request.MaxTokens,
                Stream = request.Stream,
                Messages = new List<OpenAiChatMessage>()
            };

            if (request.Messages != null)
            {
                foreach (ChatMessage message in request.Messages)
                {
                    OpenAiChatMessage openAiMessage = new OpenAiChatMessage
                    {
                        Role = ConvertRole(message.Role),
                        Content = message.Content
                    };

                    openAiRequest.Messages.Add(openAiMessage);
                }
            }

            return openAiRequest;
        }

        /// <summary>
        /// 转换 OpenAI Role。
        /// </summary>
        /// <param name="role">
        /// 统一消息角色。
        /// </param>
        /// <returns>
        /// OpenAI Role。
        /// </returns>
        private static string ConvertRole(MessageRole role)
        {
            switch (role)
            {
                case MessageRole.System:
                    return "system";

                case MessageRole.User:
                    return "user";

                case MessageRole.Assistant:
                    return "assistant";

                case MessageRole.Tool:
                    return "tool";

                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, "未知消息角色。");
            }
        }

        /// <summary>
        /// 校验 Provider 配置。
        /// </summary>
        private static void ValidateProviderOptions(AiProviderOptions providerOptions)
        {
            if (!providerOptions.Enabled)
            {
                throw new InvalidOperationException("OpenAI Provider 未启用。");
            }

            EnsureNotEmpty(providerOptions.ApiKey, nameof(providerOptions.ApiKey));
            EnsureNotEmpty(providerOptions.BaseUrl, nameof(providerOptions.BaseUrl));
            EnsureNotEmpty(providerOptions.Model, nameof(providerOptions.Model));
        }

        /// <summary>
        /// 校验字符串不能为空。
        /// </summary>
        private static void EnsureNotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{parameterName} 不能为空。", parameterName);
            }
        }
    }
}