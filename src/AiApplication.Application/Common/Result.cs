//namespace AiApplication.Application.Common
//{
//    /// <summary>
//    /// 表示一个没有返回值的操作结果。
//    ///
//    /// 职责：
//    /// 1. 描述操作是否成功。
//    /// 2. 描述失败原因。
//    /// 3. 为整个 Application 层提供统一的结果模型。
//    ///
//    /// 说明：
//    /// 以后所有业务操作，
//    /// 都应该优先返回 Result 或 Result&lt;T&gt;，
//    /// 而不是直接返回 bool。
//    /// </summary>
//    public class Result
//    {
//        /// <summary>
//        /// 是否成功。
//        /// </summary>
//        public bool Success { get; }

//        /// <summary>
//        /// 错误信息。
//        /// 成功时为空。
//        /// </summary>
//        public string ErrorMessage { get; }

//        /// <summary>
//        /// 初始化 Result。
//        /// </summary>
//        /// <param name="success">是否成功。</param>
//        /// <param name="errorMessage">错误信息。</param>
//        protected Result(bool success, string errorMessage)
//        {
//            Success = success;
//            ErrorMessage = errorMessage;
//        }

//        /// <summary>
//        /// 创建成功结果。
//        /// </summary>
//        public static Result Ok()
//        {
//            return new Result(true, null);
//        }

//        /// <summary>
//        /// 创建失败结果。
//        /// </summary>
//        public static Result Fail(string errorMessage)
//        {
//            return new Result(false, errorMessage);
//        }
//    }
//}