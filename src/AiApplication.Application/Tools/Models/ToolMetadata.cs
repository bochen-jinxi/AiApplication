using System.Collections.Generic;

namespace AiApplication.Application.Tools.Models
{
    /// <summary>
    /// Tool 元数据。
    /// </summary>
    public sealed class ToolMetadata
    {
        /// <summary>
        /// Tool 集合。
        /// </summary>
        public IList<ToolDefinition> Tools { get; set; } = new List<ToolDefinition>();
    }
}