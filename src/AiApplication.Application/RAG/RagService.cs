using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AiApplication.Application.RAG
{
    /// <summary>
    /// RAG 检索增强服务抽象。负责根据用户查询检索相关知识片段。
    /// </summary>
    public interface IRagService
    {
        /// <summary>
        /// 根据查询检索相关文档片段。
        /// </summary>
        Task<IReadOnlyList<string>> RetrieveAsync(string query, CancellationToken cancellationToken = default);
    }
}
