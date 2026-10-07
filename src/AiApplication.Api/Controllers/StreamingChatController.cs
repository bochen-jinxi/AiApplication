using AiApplication.Application.Abstractions;
using AiApplication.Application.Enums;
using AiApplication.Application.Models;
using AiApplication.Application.Models.OpenAI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public sealed class StreamingChatController : ControllerBase
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
        public StreamingChatController(IAiClientProvider clientProvider)
        {
            _clientProvider = clientProvider ?? throw new ArgumentNullException(nameof(clientProvider));
        }

        /// <summary>
        /// Streaming Chat。
        /// </summary>
        [HttpPost("stream")]
        public async Task Stream(
            [FromBody] ChatCompletionRequest request,
            CancellationToken cancellationToken)
        {
            Response.StatusCode = 200;

            Response.ContentType = "text/event-stream";

            Response.Headers["Cache-Control"] = "no-cache";

            Response.Headers["Connection"] = "keep-alive";

            Response.Headers["X-Accel-Buffering"] = "no";

            ChatCompletionRequest request2 = new ChatCompletionRequest
            {
                Temperature = (float)0.7,
                TopP = 1,
                MaxTokens = 1024,
                Stream = true,
                Messages = new System.Collections.Generic.List<ChatMessage>()
            };


            request2.Messages.Add(new ChatMessage
            {
                Role = MessageRole.User,
                Content = "你好，请介绍一下你自己。"
            });

            IAiClient _client = _clientProvider.GetClient(AiProvider.OpenAI);
            await foreach (StreamingChatChunk chunk
                in _client.StreamChatAsync(request2, cancellationToken))
            {
                if (!string.IsNullOrEmpty(chunk.Content))
                {
                    string message =
                        $"data: {chunk.Content}\n\n";

                    byte[] buffer =
                        Encoding.UTF8.GetBytes(message);

                    await Response.Body.WriteAsync(
                        buffer,
                        0,
                        buffer.Length,
                        cancellationToken);

                    await Response.Body.FlushAsync(cancellationToken);
                }
            }

            await Response.WriteAsync(
                "data: [DONE]\n\n",
                cancellationToken);

            await Response.Body.FlushAsync(cancellationToken);
        }
    }
}