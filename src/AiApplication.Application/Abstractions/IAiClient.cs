using AiApplication.Application.Models;
using AiApplication.Application.Models.OpenAI;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.Abstractions
{
    /// <summary>
    /// AI Client 接口。
    /// </summary>
    public interface IAiClient
    {
        /// <summary>
        /// 聊天补全。
        /// </summary>
        /// <param name="request">
        /// 聊天请求。
        /// </param>
        /// <param name="cancellationToken">
        /// 取消令牌。
        /// </param>
        /// <returns>
        /// 聊天结果。
        /// </returns>
        Task<ChatCompletionResult> ChatAsync(ChatCompletionRequest request, CancellationToken cancellationToken);


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
        /// Streaming Token。
        /// </returns>
      IAsyncEnumerable<StreamingChatChunk> StreamChatAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default);
    }
}