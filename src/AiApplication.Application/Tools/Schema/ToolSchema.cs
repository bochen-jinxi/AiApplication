using System.Collections.Generic;

namespace AiApplication.Application.Tools.Schema
{
    /// <summary>
    /// Tool Schema。
    /// </summary>
    public   class ToolSchema
    {
        /// <summary>
        /// 类型。
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Properties。
        /// </summary>
        public IDictionary<string, ToolSchemaProperty> Properties
        {
            get;
            set;
        }

        /// <summary>
        /// 必填参数。
        /// </summary>
        public IList<string> Required
        {
            get;
            set;
        }
    }
}