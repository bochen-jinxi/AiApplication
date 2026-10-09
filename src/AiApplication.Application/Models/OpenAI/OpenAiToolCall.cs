 
namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Tool Call。
    /// </summary>
    public sealed class OpenAiToolCall
    {
        /// <summary>
        /// Tool Call Id。
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 类型。
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Function。
        /// </summary>
        public OpenAiFunction Function
        {
            get;
            set;
        }
    }
};


 
