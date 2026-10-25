using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Tools;

namespace AiApplication.Application.Abstractions.Tools
{
    /// <summary>
    /// Tool 接口。
    /// </summary>
    public interface ITool
    {
        /// <summary>
        /// Tool 名称。
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Tool 描述。
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// 执行 Tool。
        /// </summary>
        /// <param name="context">执行上下文。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>执行结果。</returns>
        Task<ToolResult> ExecuteAsync(ToolExecutionContext context, CancellationToken cancellationToken = default);
    }
}