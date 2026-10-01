//namespace AiApplication.Application.Abstractions.Serialization
//{
//    /// <summary>
//    /// JSON 序列化抽象。
//    ///
//    /// Application 层只依赖这个接口，
//    /// 不直接依赖 System.Text.Json 或 Newtonsoft.Json。
//    ///
//    /// 后续如果要替换 JSON 实现，
//    /// 只需要替换 Infrastructure 层的具体实现，
//    /// 上层业务代码不需要修改。
//    /// </summary>
//    public interface IJsonSerializer
//    {
//        /// <summary>
//        /// 将对象序列化为 JSON 字符串。
//        /// </summary>
//        /// <typeparam name="T">对象类型。</typeparam>
//        /// <param name="value">待序列化对象。</param>
//        /// <returns>JSON 字符串。</returns>
//        string Serialize<T>(T value);

//        /// <summary>
//        /// 将 JSON 字符串反序列化为对象。
//        /// 反序列化失败时抛出异常。
//        /// </summary>
//        /// <typeparam name="T">目标类型。</typeparam>
//        /// <param name="json">JSON 字符串。</param>
//        /// <returns>反序列化后的对象。</returns>
//        T Deserialize<T>(string json);

//        /// <summary>
//        /// 尝试将 JSON 字符串反序列化为对象。
//        /// 失败时不抛异常，而是返回结果对象。
//        /// </summary>
//        /// <typeparam name="T">目标类型。</typeparam>
//        /// <param name="json">JSON 字符串。</param>
//        /// <returns>反序列化结果。</returns>
//        AiApplication.Application.Serialization.JsonDeserializeResult<T> TryDeserialize<T>(string json);
//    }
//}