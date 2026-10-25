using System;
using AiApplication.Application.Abstractions;
using AiApplication.Application.Abstractions.Tools;
using AiApplication.Application.Tools;

namespace AiApplication.Infrastructure.Tools
{
    /// <summary>
    /// Tool 参数绑定器。
    /// </summary>
    public sealed class ToolParameterBinder : IToolParameterBinder
    {
        private readonly IAiSerializer _serializer;

        /// <summary>
        /// 初始化参数绑定器。
        /// </summary>
        /// <param name="serializer">序列化器。</param>
        public ToolParameterBinder(IAiSerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <summary>
        /// 绑定 Tool 参数。
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="context">Tool 执行上下文。</param>
        /// <returns>参数对象。</returns>
        public T Bind<T>(ToolExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrWhiteSpace(context.Arguments))
            {
                throw new InvalidOperationException("Tool 参数不能为空。");
            }

            T result = _serializer.Deserialize<T>(context.Arguments);

            if (result == null)
            {
                throw new InvalidOperationException("Tool 参数反序列化失败。");
            }

            return result;
        }
    }
}