using AiApplication.Application.Abstractions;
using AiApplication.Application.Enums;
using AiApplication.Application.Models;
using AiApplication.Application.Models.OpenAI;
using AiApplication.Application.Options;
using AiApplication.Application.Tools.Models;
using AiApplication.Infrastructure.AI.OpenAI.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        /// 日志。
        /// </summary>
        private readonly ILogger<OpenAiClient> _logger;

        private readonly OpenAiToolBuilder _toolBuilder;
        /// <summary>
        /// 初始化 OpenAI Client。
        /// </summary>
        public OpenAiClient(
      IAiHttpTransport httpTransport,
      IAiSerializer serializer,
      IOptions<AiOptions> options,
      ILogger<OpenAiClient> logger,
      OpenAiToolBuilder toolBuilder)
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _toolBuilder = toolBuilder;
        }

        private List<OpenAiTool> BuildTools(IList<ToolDefinition> tools)
        {
            if (tools == null || tools.Count == 0)
            {
                return null;
            }

            return _toolBuilder.Build(tools).ToList();
        }

        /// <summary>
        /// 读取 OpenAI 响应。
        /// </summary>
        /// <param name="responseMessage">
        /// HTTP 响应。
        /// </param>
        /// <returns>
        /// OpenAI 响应对象。
        /// </returns>
        private async Task<OpenAiChatCompletionResponse> ReadResponseAsync(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
            {
                throw new ArgumentNullException(nameof(responseMessage));
            }
            /*
           {"id":"chatcmpl-da76ee9d31ed4867b83ad0ef",
          "object":"chat.completion",
          "created":1790847129,
          "model":"gpt-5.6-luna",
          "choices":[{"index":0,"message":{"role":"assistant","content":"你好！我是 ChatGPT，由 OpenAI 训练的人工智能助手。\n\n我可以帮助你：\n- 解答问题、讲解知识\n- 撰写和修改文章、邮件、报告\n- 翻译、总结和提炼信息\n- 协助编程、调试代码\n- 制定学习、工作或旅行计划\n- 进行头脑风暴和日常对话\n\n我会尽力提供准确、清晰、实用的回答；如果信息可能过时或需要核实，我也会提醒你。很高兴认识你！"},"finish_reason":"stop"}],
          "usage":{"prompt_tokens":23,"completion_tokens":126,"total_tokens":149,"prompt_tokens_details":{"cached_tokens":0},"completion_tokens_details":{"reasoning_tokens":0}}}
           */

            /*
             {"id":"chatcmpl-ffaba82f46d3469780468615","object":"chat.completion","created":1791705457,"model":"gpt-5.6-luna","choices":[{"index":0,"message":{"role":"assistant","content":null,"tool_calls":[{"id":"call_17b05fe59ba94b358e9d801a329c5f5b","type":"function","function":{"name":"weather","arguments":"{\"City\":\"成都\",\"Days\":1}"}}]},"finish_reason":"tool_calls"}],"usage":{"prompt_tokens":70,"completion_tokens":21,"total_tokens":91,"prompt_tokens_details":{"cached_tokens":0},"completion_tokens_details":{"reasoning_tokens":0}}}
             
             */
            string responseJson =
                await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

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

            return response;
        }

        /// <summary>
        /// 创建 HTTP 请求。
        /// </summary>
        /// <param name="url">
        /// 请求地址。
        /// </param>
        /// <param name="json">
        /// 请求 JSON。
        /// </param>
        /// <param name="apiKey">
        /// API Key。
        /// </param>
        /// <returns>
        /// HttpRequestMessage。
        /// </returns>
        private static HttpRequestMessage BuildHttpRequestMessage(
            string url,
            string json,
            string apiKey)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            return request;
        }

        private OpenAiChatCompletionRequest BuildRequest(ChatCompletionRequest chatRequest)
        {
            OpenAiChatCompletionRequest openAiRequest = new OpenAiChatCompletionRequest
            {
                Model = _providerOptions.Model,
                Temperature = chatRequest.Temperature,
                TopP = chatRequest.TopP,
                MaxTokens = chatRequest.MaxTokens,
                Stream = chatRequest.Stream,
                Messages = new List<OpenAiChatMessage>(),
                Tools = BuildTools(chatRequest.Tools)
            };

            if (chatRequest.Messages != null)
            {
                foreach (ChatMessage message in chatRequest.Messages)
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
        /// 聊天补全。
        /// </summary>
        public async Task<ChatCompletionResult> ChatAsync(
     ChatCompletionRequest request,
     CancellationToken cancellationToken)
        {
            _logger.LogInformation(
    "OpenAI Chat 请求开始。");
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            OpenAiChatCompletionRequest openAiRequest = BuildRequest(request);
            /*
             {"model":"gpt-5.6-luna","messages":[{"role":"user","content":"你好，请介绍一下你自己。"}],"temperature":0.7,"max_tokens":1024,"top_p":1,"stream":false}
             */

            /*
             {"model":"gpt-5.6-luna","messages":[{"role":"user","content":"成都今天天气怎么样？"}],"temperature":0.7,"max_tokens":1024,"top_p":1,"stream":false,"tools":[{"type":"function","function":{"name":"weather","description":"获取指定城市天气","parameters":{"type":"object","properties":{"city":{"type":"string","description":"城市名称"},"days":{"type":"integer","description":"预测天数"}},"required":["city"]}}}]}
             */
            string requestJson = _serializer.Serialize(openAiRequest);
          

            var requestMessage = BuildHttpRequestMessage(_providerOptions.BaseUrl,
                 requestJson,
                 _providerOptions.ApiKey);
            var responseJson = await _httpTransport.SendAsync(
                requestMessage,
                cancellationToken);

      



            OpenAiChatCompletionResponse response =
    await ReadResponseAsync(responseJson);

            OpenAiChoice choice = response.Choices[0];
            if (choice.FinishReason == "tool_calls")
            {
                List<OpenAiToolCall> toolCalls = choice.Message.ToolCalls;

                // 下一节开始执行 Tool
            }

            ChatCompletionResult result = new ChatCompletionResult
            {
                Content = choice.Message.Content,
                FinishReason = choice.FinishReason,
                PromptTokens = response.Usage?.PromptTokens ?? 0,
                CompletionTokens = response.Usage?.CompletionTokens ?? 0,
                TotalTokens = response.Usage?.TotalTokens ?? 0
            };

            _logger.LogInformation(
    "OpenAI Chat 请求完成。");
            return result;
        }



        /// <summary>
        /// Streaming Chat。
        /// </summary>
        /// <param name="request">
        /// 聊天请求。
        /// </param>
        /// <param name="cancellationToken">
        /// CancellationToken。
        /// </param>
        /// <returns>
        /// Streaming Chunk。
        /// </returns>
        public async IAsyncEnumerable<StreamingChatChunk> StreamChatAsync(
            ChatCompletionRequest request,
            [System.Runtime.CompilerServices.EnumeratorCancellation]
    CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.Stream = true;

            OpenAiChatCompletionRequest openAiRequest = BuildRequest(request);
            /*{"model":"gpt-5.6-luna","messages":[{"role":"user","content":"\u4F60\u597D\uFF0C\u8BF7\u4ECB\u7ECD\u4E00\u4E0B\u4F60\u81EA\u5DF1\u3002"}],"temperature":0.7,"max_tokens":1024,"top_p":1,"stream":true}*/
            string requestJson =
                _serializer.Serialize(openAiRequest);

            HttpRequestMessage requestMessage =
                BuildHttpRequestMessage(
                    _providerOptions.BaseUrl,
                    requestJson,
                    _providerOptions.ApiKey);

            HttpResponseMessage response =
                await _httpTransport.SendAsync(
                    requestMessage,
                    cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            using Stream stream =
                await response.Content
                    .ReadAsStreamAsync()
                    .ConfigureAwait(false);

            using StreamReader reader =
                new StreamReader(stream);

            string line;
           
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (!line.StartsWith("data:"))
                {
                    continue;
                }
                /*第一个流内容line = data: {"id":"chatcmpl-8485248bcc6541e09b4377d2","object":"chat.completion.chunk","created":1791363933,"model":"gpt-5.6-luna","choices":[{"index":0,"delta":{"role":"assistant"},"finish_reason":null}]}*/
                /*第二个流内容line = data:{ "id":"chatcmpl-9fb335d114054a8ba75d8639","object":"chat.completion.chunk","created":1791685316,"model":"gpt-6-luna","choices":[{ "index":0,"delta":{ "content":"你好"},"finish_reason":null}]}*/

                string json =
                    line.Substring(5).Trim();

                if (json == "[DONE]")
                {
                    yield break;
                }

                OpenAiChatCompletionChunk chunk =
                    _serializer.Deserialize<OpenAiChatCompletionChunk>(json);

                if (chunk == null)
                {
                    continue;
                }

                if (chunk.Choices == null ||
                    chunk.Choices.Count == 0)
                {
                    continue;
                }
                //第一个内容{"index":0,"delta":{"role":"assistant"},"finish_reason":null}
                //第二个内容{ "index":0,"delta":{ "content":"你好"},"finish_reason":null}
                OpenAiChunkChoice choice =
                    chunk.Choices[0];
                //第一个内容{"role":"assistant"}
                //第二个内容{ "content":"你好"}
                OpenAiDelta delta =
                    choice.Delta;

                yield return new StreamingChatChunk
                {
                    Role = delta?.Role,
                    Content = delta?.Content,
                    FinishReason = choice.FinishReason,
                    IsCompleted = !string.IsNullOrWhiteSpace(choice.FinishReason)
                };
            }
        }


        /// <summary>
        /// 构建 OpenAI 请求对象。
        /// </summary>
 

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