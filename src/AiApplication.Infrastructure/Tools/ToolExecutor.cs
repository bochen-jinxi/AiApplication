using System;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions;
using AiApplication.Application.Abstractions.Tools;
using AiApplication.Application.Tools;

namespace AiApplication.Infrastructure.Tools
{
    /// <summary>
    /// Tool 执行器。
    /// </summary>
    public sealed class ToolExecutor : IToolExecutor
    {
        private readonly IToolRegistry _toolRegistry;
        private readonly IAiSerializer _serializer;

        public ToolExecutor(
            IToolRegistry toolRegistry,
            IAiSerializer serializer)
        {
            _toolRegistry = toolRegistry ?? throw new ArgumentNullException(nameof(toolRegistry));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <summary>
        /// 执行 Tool。
        /// </summary>
        /// <param name="context">执行上下文。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>ToolResult。</returns>
        public async Task<ToolResult> ExecuteAsync(
            ToolExecutionContext context,
            CancellationToken cancellationToken = default)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ITool tool = _toolRegistry.Get(context.ToolName);

            if (tool == null)
            {
                return ToolResult.Fail($"未找到 Tool：{context.ToolName}");
            }

            try
            {
                ToolResult result = await tool.ExecuteAsync(
                    context,
                    cancellationToken);

                return result;
            }
            catch (Exception exception)
            {
                return ToolResult.Fail(exception.Message);
            }
        }


      
    }
}