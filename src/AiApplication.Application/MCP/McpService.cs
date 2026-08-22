using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.MCP
{
    /// <summary>
    /// MCP 工具服务抽象。负责执行模型请求的工具调用并返回结果。
    /// </summary>
    public interface IMcpService
    {
        /// <summary>
        /// 根据用户消息判断是否需要执行工具，并返回工具调用结果列表。
        /// </summary>
        Task<IReadOnlyList<string>> ExecuteToolsAsync(string userMessage, CancellationToken cancellationToken = default);
    }
}
