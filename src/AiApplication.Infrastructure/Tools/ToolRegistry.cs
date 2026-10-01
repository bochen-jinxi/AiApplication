//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AiApplication.Application.Abstractions.Tools;

//namespace AiApplication.Infrastructure.Tools
//{
//    public sealed class ToolRegistry : IToolRegistry
//    {
//        private readonly Dictionary<string, ITool> _toolDictionary;

//        public ToolRegistry(IEnumerable<ITool> tools)
//        {
//            _toolDictionary = tools.ToDictionary(tool => tool.Name, StringComparer.OrdinalIgnoreCase);
//        }

//        public ITool GetTool(string toolName)
//        {
//            if (!_toolDictionary.TryGetValue(toolName, out var tool))
//            {
//                throw new InvalidOperationException(string.Format("Tool '{0}' does not exist.", toolName));
//            }

//            return tool;
//        }

//        public bool TryGetTool(string toolName, out ITool tool)
//        {
//            return _toolDictionary.TryGetValue(toolName, out tool);
//        }
//    }
//}