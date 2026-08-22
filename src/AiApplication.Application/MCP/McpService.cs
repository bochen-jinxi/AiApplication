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

    /// <summary>
    /// MCP 服务默认实现。
    /// 当前为占位实现，直接返回空列表，表示未触发任何工具调用。
    /// 后续接入 MCP 协议客户端后，替换为真实工具执行逻辑。
    /// </summary>
    public sealed class McpService : IMcpService
    {
        public Task<IReadOnlyList<string>> ExecuteToolsAsync(string userMessage, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<string> empty = System.Array.Empty<string>();
            return Task.FromResult(empty);
        }
    }
}
