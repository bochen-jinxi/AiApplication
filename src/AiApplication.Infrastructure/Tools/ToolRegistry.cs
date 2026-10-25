using System;
using System.Collections.Generic;
using AiApplication.Application.Abstractions.Tools;

namespace AiApplication.Infrastructure.Tools
{
    /// <summary>
    /// Tool 注册中心。
    /// </summary>
    public sealed class ToolRegistry : IToolRegistry
    {
        private readonly Dictionary<string, ITool> _tools;

        /// <summary>
        /// 初始化 Tool 注册中心。
        /// </summary>
        /// <param name="tools">所有 Tool。</param>
        public ToolRegistry(IEnumerable<ITool> tools)
        {
            if (tools == null)
            {
                throw new ArgumentNullException(nameof(tools));
            }

            _tools = new Dictionary<string, ITool>(StringComparer.OrdinalIgnoreCase);

            foreach (ITool tool in tools)
            {
                if (tool == null)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(tool.Name))
                {
                    throw new InvalidOperationException("Tool.Name 不能为空。");
                }

                if (_tools.ContainsKey(tool.Name))
                {
                    throw new InvalidOperationException($"Tool 已重复注册：{tool.Name}");
                }

                _tools.Add(tool.Name, tool);
            }
        }

        /// <summary>
        /// 所有 Tool。
        /// </summary>
        public IReadOnlyCollection<ITool> Tools
        {
            get
            {
                return _tools.Values;
            }
        }

        /// <summary>
        /// 根据名称获取 Tool。
        /// </summary>
        /// <param name="toolName">Tool 名称。</param>
        /// <returns>Tool。</returns>
        public ITool Get(string toolName)
        {
            if (string.IsNullOrWhiteSpace(toolName))
            {
                return null;
            }

            _tools.TryGetValue(toolName, out ITool tool);

            return tool;
        }
    }
}