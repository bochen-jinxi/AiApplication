using System.Collections.Generic;
using AiApplication.Application.Models.OpenAI;
namespace AiApplication.Infrastructure.AI.OpenAI.Dtos
{
    /// <summary>
    /// OpenAI Message。
    /// </summary>
    public sealed class OpenAiMessage
    {
        /// <summary>
        /// Role。
        /// </summary>
        public string Role 
        {
            get;
            set;
        }

        /// <summary>
        /// Content。
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Tool Calls。
        /// </summary>
        public IList<OpenAiToolCall> ToolCalls
        {
            get;
            set;
        }
    }
}