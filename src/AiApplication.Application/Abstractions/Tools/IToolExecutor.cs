using AiApplication.Application.Tools;
using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.Abstractions.Tools
{
    /// <summary>
    /// Tool 执行器。
    /// </summary>
    public interface IToolExecutor
    {
        /// <summary>
        /// 执行 Tool。
        /// </summary>
        Task<ToolResult> ExecuteAsync(ToolExecutionContext context, CancellationToken cancellationToken = default);
    }
}