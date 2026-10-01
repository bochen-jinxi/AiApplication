using System;
using System.Text.Json;
using AiApplication.Application.Abstractions;

namespace AiApplication.Infrastructure.Serialization
{
    /// <summary>
    /// System.Text.Json 序列化器。
    /// </summary>
    public sealed class SystemTextJsonSerializer : IAiSerializer
    {
        /// <summary>
        /// JSON 配置。
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// 初始化序列化器。
        /// </summary>
        /// <param name="jsonSerializerOptions">
        /// JSON 配置。
        /// </param>
        public SystemTextJsonSerializer(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
        }

        /// <summary>
        /// 将对象序列化为 JSON。
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型。
        /// </typeparam>
        /// <param name="value">
        /// 待序列化对象。
        /// </param>
        /// <returns>
        /// JSON 字符串。
        /// </returns>
        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, _jsonSerializerOptions);
        }

        /// <summary>
        /// 将 JSON 反序列化为对象。
        /// </summary>
        /// <typeparam name="T">
        /// 对象类型。
        /// </typeparam>
        /// <param name="json">
        /// JSON 字符串。
        /// </param>
        /// <returns>
        /// 对象实例。
        /// </returns>
        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("JSON 字符串不能为空。", nameof(json));
            }

            T result = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

            if (result == null)
            {
                throw new InvalidOperationException("JSON 反序列化失败。");
            }

            return result;
        }
    }
}
