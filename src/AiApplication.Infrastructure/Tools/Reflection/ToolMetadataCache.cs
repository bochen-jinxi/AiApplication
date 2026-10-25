using System;
using System.Collections.Concurrent;

namespace AiApplication.Infrastructure.Tools.Reflection
{
    /// <summary>
    /// Tool 元数据缓存。
    /// </summary>
    public sealed class ToolMetadataCache
    {
        private readonly ConcurrentDictionary<Type, object> _cache = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 获取缓存。
        /// </summary>
        /// <typeparam name="T">缓存类型。</typeparam>
        /// <param name="toolType">Tool 类型。</param>
        /// <param name="factory">创建方法。</param>
        /// <returns>缓存对象。</returns>
        public T GetOrAdd<T>(Type toolType, Func<T> factory)
        {
            if (toolType == null)
            {
                throw new ArgumentNullException(nameof(toolType));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            object value = _cache.GetOrAdd(toolType, _ => factory());

            return (T)value;
        }
    }
}