using System;
using System.Collections.Generic;

namespace AiApplication.Application.Tools.Models
{
    /// <summary>
    /// Tool 定义。
    /// </summary>
    public sealed class ToolDefinition
    {
        /// <summary>
        /// Tool 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tool 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 请求类型。
        /// </summary>
        public Type RequestType { get; set; }

        /// <summary>
        /// 返回类型。
        /// </summary>
        public Type ResponseType { get; set; }

        /// <summary>
        /// 参数定义。
        /// </summary>
        public IList<ToolParameterDefinition> Parameters { get; set; } = new List<ToolParameterDefinition>();
    }
}