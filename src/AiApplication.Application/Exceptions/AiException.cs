using System;

namespace AiApplication.Application.Exceptions
{
    /// <summary>
    /// AI 平台异常。
    /// </summary>
    public class AiException : Exception
    {
        /// <summary>
        /// 初始化 AI 平台异常。
        /// </summary>
        public AiException()
        {
        }

        /// <summary>
        /// 初始化 AI 平台异常。
        /// </summary>
        /// <param name="message">
        /// 异常信息。
        /// </param>
        public AiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 初始化 AI 平台异常。
        /// </summary>
        /// <param name="message">
        /// 异常信息。
        /// </param>
        /// <param name="innerException">
        /// 内部异常。
        /// </param>
        public AiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}