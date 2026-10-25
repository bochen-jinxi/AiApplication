using System;

namespace AiApplication.Application.Tools
{
    /// <summary>
    /// Tool 执行上下文。
    /// </summary>
    public sealed class ToolExecutionContext
    {
        /// <summary>
        /// Tool 名称。
        /// </summary>
        public string ToolName
        {
            get;
            set;
        }

        /// <summary>
        /// Tool 调用 Id。
        /// </summary>
        public string ToolCallId
        {
            get;
            set;
        }

        /// <summary>
        /// Tool 参数(Json)。
        /// </summary>
        public string Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// 当前会话 Id。
        /// </summary>
        public string ConversationId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前用户 Id。
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
    }
}