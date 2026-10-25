using AiApplication.Application.Tools;

namespace AiApplication.Application.Abstractions.Tools
{
    /// <summary>
    /// Tool 参数绑定器。
    /// </summary>
    public interface IToolParameterBinder
    {
        /// <summary>
        /// 从 Tool 执行上下文绑定参数对象。
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="context">Tool 执行上下文。</param>
        /// <returns>参数对象。</returns>
        T Bind<T>(ToolExecutionContext context);
    }
}