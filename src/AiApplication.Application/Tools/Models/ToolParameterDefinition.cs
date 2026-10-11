namespace AiApplication.Application.Tools.Models
{
    /// <summary>
    /// Tool 参数定义。
    /// </summary>
    public sealed class ToolParameterDefinition
    {
        /// <summary>
        /// 参数名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数类型。
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 参数描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否必填。
        /// </summary>
        public bool Required { get; set; }
    }
}