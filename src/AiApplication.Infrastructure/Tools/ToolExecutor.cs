// using System.Collections.Generic;

// namespace AiApplication.Infrastructure.Tools;

// public class ToolExecutor
// {

//     private readonly IReadOnlyDictionary<string, ITool> _tools;

//     public ToolExecutor(
//         IEnumerable<ITool> tools)
//     {
//         Dictionary<string, ITool> dictionary =
//             new Dictionary<string, ITool>(
//                 StringComparer.OrdinalIgnoreCase);

//         foreach (ITool tool in tools)
//         {
//             if (dictionary.ContainsKey(tool.Name))
//             {
//                 throw new ToolException(
//                     $"Tool '{tool.Name}' 已经存在。");
//             }

//             dictionary.Add(
//                 tool.Name,
//                 tool);
//         }

//         _tools = dictionary;
//     }

// public async Task<ToolResult> ExecuteAsync(
//     string toolName,
//     ToolContext context)
// {
//     if (!_tools.TryGetValue(
//             toolName,
//             out ITool tool))
//     {
//         throw new ToolException(
//             $"Tool '{toolName}' 不存在。");
//     }

//     return await tool.ExecuteAsync(
//         context)
//         .ConfigureAwait(false);
// }
// }
