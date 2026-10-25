using System;

namespace AiApplication.Application.Tools
{
    /// <summary>
    /// Tool 执行结果。
    /// </summary>
    public sealed class ToolResult
    {
        /// <summary>
        /// 是否执行成功。
        /// </summary>
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// 返回给 LLM 的内容。
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 创建成功结果。
        /// </summary>
        /// <param name="content">返回内容。</param>
        /// <returns>ToolResult。</returns>
        public static ToolResult Ok(string content)
        {
            return new ToolResult
            {
                Success = true,
                Content = content,
                ErrorMessage = null
            };
        }

        /// <summary>
        /// 创建失败结果。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <returns>ToolResult。</returns>
        public static ToolResult Fail(string errorMessage)
        {
            return new ToolResult
            {
                Success = false,
                Content = null,
                ErrorMessage = errorMessage
            };
        }
    }
}