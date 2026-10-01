namespace AiApplication.Application.Abstractions
{
    /// <summary>
    /// AI JSON 序列化器。
    /// </summary>
    public interface IAiSerializer
    {
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
        string Serialize<T>(T value);

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
        T Deserialize<T>(string json);
    }
}