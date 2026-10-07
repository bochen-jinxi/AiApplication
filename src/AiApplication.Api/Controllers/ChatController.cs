using AiApplication.Application.Abstractions;
using AiApplication.Application.Enums;
using AiApplication.Application.Models;
using AiApplication.Application.Models.OpenAI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.WebApi.Controllers
{
    /// <summary>
    /// AI 聊天接口。
    /// </summary>
    [ApiController]
    [Route("api/chat")]
    public sealed class ChatController : ControllerBase
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
        public ChatController(IAiClientProvider clientProvider)
        {
            _clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
        }

        /// <summary>
        /// 聊天。
        /// </summary>
        /// <returns>
        /// ChatCompletionResult。
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ChatCompletionResult>> Chat()
        {
            ChatCompletionRequest request = new ChatCompletionRequest
            {
                Temperature = (float)0.7,
                TopP = 1,
                MaxTokens = 1024,
                Stream = false,
                Messages = new System.Collections.Generic.List<ChatMessage>()
            };


            request.Messages.Add(new ChatMessage
            {
                Role = MessageRole.User,
                Content = "你好，请介绍一下你自己。"
            });

            IAiClient client = _clientProvider.GetClient(AiProvider.OpenAI);

            ChatCompletionResult result = await client.ChatAsync(request, CancellationToken.None);

            return Ok(result);
        }
         
    }
}