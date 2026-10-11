using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AiApplication.Application.Tools.Reflection
{
    /// <summary>
    /// Reflection Metadata Builder。
    /// </summary>
    public sealed class ReflectionMetadataBuilder
    {
        /// <summary>
        /// 构建元数据。
        /// </summary>
        public IList<ReflectionMetadata> Build(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            List<ReflectionMetadata> result = new List<ReflectionMetadata>();

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                ReflectionMetadata metadata = new ReflectionMetadata
                {
                    Name = property.Name,
                    Type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType,
                    Nullable = Nullable.GetUnderlyingType(property.PropertyType) != null
                };

                DescriptionAttribute descriptionAttribute = property.GetCustomAttribute<DescriptionAttribute>();

                if (descriptionAttribute != null)
                {
                    metadata.Description = descriptionAttribute.Description;
                }

                RequiredAttribute requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();

                metadata.Required = requiredAttribute != null;

                result.Add(metadata);
            }

            return result;
        }
    }
}