using AiApplication.Application.Abstractions;
using AiApplication.Application.Enums;
using AiApplication.Application.Models;
using AiApplication.Application.Models.OpenAI;
using AiApplication.Application.Tools.Models;
using AiApplication.Application.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Controllers
{
    /// <summary>
    /// Streaming Chat。
    /// </summary>
    [ApiController]
    [Route("api/chat")]
    public sealed class TollCallingChatController : ControllerBase
    {
        /// <summary>
        /// AI Client Provider。
        /// </summary>
        private readonly IAiClientProvider _clientProvider;

        /// <summary>
        /// 初始化 Controller。
        /// </summary>
        /// <param name="clientProvider">
        /// AI Client Provider。
        /// </param>
        public TollCallingChatController(IAiClientProvider clientProvider)
        {
            _clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
        }

        /// <summary>
        /// Streaming Chat。
        /// </summary>
        [HttpPost("toolcalling")]
        public async Task<ActionResult<ChatCompletionResult>> Stream(
 
            CancellationToken cancellationToken)
        {
             

            ChatCompletionRequest request3 = new ChatCompletionRequest
            {
                Temperature = (float)0.7,
                TopP = 1,
                MaxTokens = 1024,
                Stream = false,
                Messages = new System.Collections.Generic.List<ChatMessage>() { new ChatMessage
                {
                    Role = MessageRole.User,
                    Content = "成都今天天气怎么样？"
                }},
                Tools = new List<ToolDefinition>
                {
                    new ToolDefinition{ Name="weather", Description="获取指定城市天气", RequestType=typeof(WeatherRequest) }
                }
            };


            

            IAiClient _client = _clientProvider.GetClient(AiProvider.OpenAI);
            ChatCompletionResult result = await _client.ChatAsync(request3, CancellationToken.None);

            return Ok(result);
        }
    }
}