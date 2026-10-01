//namespace AiApplication.Domain.AI
//{
//    /// <summary>
//    /// AI 对话消息。统一表示 system / user / assistant / tool 四种角色的消息内容。
//    /// </summary>
//    public sealed class AiMessage
//    {
//        public AiRoleEnum Role { get; set; }

//        public string Content { get; set;}

//        /// <summary>
//        /// 当 <see cref="Role"/> 为 <see cref="AiRoleEnum.Tool"/> 时，标识工具调用的名称或 ID。
//        /// 其他角色下为 null。
//        /// </summary>
//        public string ToolName { get;set; }

//        public AiMessage()
//        { }
//        public AiMessage(AiRoleEnum role, string content, string toolName = null)
//        {
//            if (content == null)
//            {
//                throw new System.ArgumentNullException(nameof(content));
//            }

//            Role = role;
//            Content = content;
//            ToolName = toolName;
//        }

//        public static AiMessage System(string content) => new AiMessage(AiRoleEnum.System, content);

//        public static AiMessage User(string content) => new AiMessage(AiRoleEnum.User, content);

//        public static AiMessage Assistant(string content) => new AiMessage(AiRoleEnum.Assistant, content);

//        public static AiMessage Tool(string name, string content) => new AiMessage(AiRoleEnum.Tool, content, name);

//        public override string ToString() => $"[{Role}] {Content}";
//    }
//}
