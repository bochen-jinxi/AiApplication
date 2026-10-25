using System.Collections.Generic;

namespace AiApplication.Application.Abstractions.Tools
{
    /// <summary>
    /// Tool 注册中心。
    /// </summary>
    public interface IToolRegistry
    {
        /// <summary>
        /// 获取所有 Tool。
        /// </summary>
        IReadOnlyCollection<ITool> Tools { get; }

        /// <summary>
        /// 根据名称获取 Tool。
        /// </summary>
        /// <param name="toolName">Tool 名称。</param>
        /// <returns>Tool。</returns>
        ITool Get(string toolName);
    }
}