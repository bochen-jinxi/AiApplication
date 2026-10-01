using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.Abstractions
{
    /// <summary>
    /// Tool 执行器。
    /// </summary>
    public interface IToolExecutor
    {
        /// <summary>
        /// 执行 Tool。
        /// </summary>
        /// <param name="toolName">
        /// Tool 名称。
        /// </param>
        /// <param name="arguments">
        /// Tool 参数(JSON)。
        /// </param>
        /// <param name="cancellationToken">
        /// 取消令牌。
        /// </param>
        /// <returns>
        /// Tool 执行结果(JSON)。
        /// </returns>
        Task<string> ExecuteAsync(string toolName, string arguments, CancellationToken cancellationToken);
    }
}