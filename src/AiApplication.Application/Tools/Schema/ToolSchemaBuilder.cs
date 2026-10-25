using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AiApplication.Application.Tools.Schema
{
    /// <summary>
    /// Tool Schema Builder。
    /// </summary>
    public static class ToolSchemaBuilder
    {
        /// <summary>
        /// 构建 Schema。
        /// </summary>
        public static ToolSchema Build(Type requestType)
        {
            if (requestType == null)
            {
                throw new ArgumentNullException(nameof(requestType));
            }

            ToolSchema schema = new ToolSchema
            {
                Type = "object",
                Properties = new Dictionary<string, ToolSchemaProperty>(),
                Required = new List<string>()
            };

            PropertyInfo[] properties = requestType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                ToolSchemaProperty schemaProperty =
                    new ToolSchemaProperty();

                schemaProperty.Type =
                    TypeMapper.Map(property.PropertyType);

                // 读取 [Description] 特性，填充属性描述
                DescriptionAttribute descriptionAttribute =
                    property.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                {
                    schemaProperty.Description =
                        descriptionAttribute.Description;
                }

                // 读取 [Required] 特性，收集必填属性名
                RequiredAttribute requiredAttribute =
                    property.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttribute != null)
                {
                    schema.Required.Add(property.Name);
                }

                schema.Properties.Add(
                    property.Name,
                    schemaProperty);
            }

            return schema;
        }
    }
}