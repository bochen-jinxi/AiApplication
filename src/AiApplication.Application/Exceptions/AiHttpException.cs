using System.Net;

namespace AiApplication.Application.Exceptions
{
    /// <summary>
    /// AI HTTP 异常。
    /// </summary>
    public class AiHttpException : AiException
    {
        /// <summary>
        /// HTTP 状态码。
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// 初始化 HTTP 异常。
        /// </summary>
        /// <param name="statusCode">
        /// HTTP 状态码。
        /// </param>
        /// <param name="message">
        /// 异常信息。
        /// </param>
        public AiHttpException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// 初始化 HTTP 异常。
        /// </summary>
        /// <param name="statusCode">
        /// HTTP 状态码。
        /// </param>
        /// <param name="message">
        /// 异常信息。
        /// </param>
        /// <param name="innerException">
        /// 内部异常。
        /// </param>
        public AiHttpException(HttpStatusCode statusCode, string message, System.Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}