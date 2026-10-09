namespace AiApplication.Application.Models.OpenAI
{
    /// <summary>
    /// OpenAI Function。
    /// </summary>
    public sealed class OpenAiFunction
    {
        /// <summary>
        /// Function 名称。
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Function 参数(JSON)。
        /// </summary>
        public string Arguments
        {
            get;
            set;
        }
    }
}