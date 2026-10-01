//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using AiApplication.Application.Abstractions.Tools;

//namespace AiApplication.Application.Tools
//{
//    public sealed class ToolExecutor : IToolExecutor
//    {
//        private readonly IToolRegistry _toolRegistry;

//        public ToolExecutor(IToolRegistry toolRegistry)
//        {
//            _toolRegistry = toolRegistry ?? throw new ArgumentNullException(nameof(toolRegistry));
//        }

//        public async Task<ToolResult> ExecuteAsync(ToolCall toolCall, CancellationToken cancellationToken)
//        {
//            if (toolCall == null)
//            {
//                throw new ArgumentNullException(nameof(toolCall));
//            }

//            if (!_toolRegistry.TryGetTool(toolCall.ToolName, out var tool))
//            {
//                return new ToolResult
//                {
//                    Success = false,
//                    Content = string.Empty,
//                    ErrorMessage = $"Tool '{toolCall.ToolName}' was not found."
//                };
//            }

//            return await tool.ExecuteAsync(toolCall, cancellationToken);
//        }
//    }
//}