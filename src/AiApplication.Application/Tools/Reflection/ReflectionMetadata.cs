using System;

namespace AiApplication.Application.Tools.Reflection
{
    /// <summary>
    /// 反射元数据。
    /// </summary>
    public sealed class ReflectionMetadata
    {
        /// <summary>
        /// 属性名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性类型。
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否必填。
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 是否可空。
        /// </summary>
        public bool Nullable { get; set; }
    }
}