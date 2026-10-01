//using System;

//namespace AiApplication.Application.Serialization
//{
//    /// <summary>
//    /// JSON 反序列化结果。
//    ///
//    /// 职责：
//    /// 1. 表示反序列化是否成功。
//    /// 2. 保存反序列化后的对象。
//    /// 3. 保存失败原因。
//    ///
//    /// 说明：
//    /// 对于来自 LLM、HTTP、消息队列、用户输入等不可信数据，
//    /// 失败是正常业务场景，因此应返回结果对象，
//    /// 而不是让调用方到处写 try/catch。
//    /// </summary>
//    /// <typeparam name="T">反序列化后的对象类型。</typeparam>
//    public sealed class JsonDeserializeResult<T>
//    {
//        /// <summary>
//        /// 是否成功。
//        /// </summary>
//        public bool Success { get; }

//        /// <summary>
//        /// 反序列化后的对象。
//        /// </summary>
//        public T Value { get; }

//        /// <summary>
//        /// 错误信息。
//        /// 成功时为 null。
//        /// </summary>
//        public string ErrorMessage { get; }

//        private JsonDeserializeResult(bool success, T value, string errorMessage)
//        {
//            Success = success;
//            Value = value;
//            ErrorMessage = errorMessage;
//        }

//        /// <summary>
//        /// 创建成功结果。
//        /// </summary>
//        /// <param name="value">反序列化后的对象。</param>
//        public static JsonDeserializeResult<T> Ok(T value)
//        {
//            return new JsonDeserializeResult<T>(true, value, null);
//        }

//        /// <summary>
//        /// 创建失败结果。
//        /// </summary>
//        /// <param name="errorMessage">失败原因。</param>
//        public static JsonDeserializeResult<T> Fail(string errorMessage)
//        {
//            return new JsonDeserializeResult<T>(false, default(T), errorMessage);
//        }
//    }
//}