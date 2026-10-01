//namespace AiApplication.Infrastructure.AI.DeepSeek
//{
//    /// <summary>
//    /// DeepSeek 客户端配置。从 appsettings.json 的 "AI:DeepSeek" 节点绑定。
//    /// DeepSeek 兼容 OpenAI Chat Completions 协议，仅 BaseUrl 与默认模型不同。
//    /// </summary>
//    public sealed class DeepSeekOptions
//    {
//        public const string SectionName = "AI:DeepSeek";

//        public string ApiKey { get; set; }

//        public string BaseUrl { get; set; } = "https://api.deepseek.com/v1";

//        public string DefaultModel { get; set; } = "deepseek-chat";

//        public double DefaultTemperature { get; set; } = 0.7;

//        public int? DefaultMaxTokens { get; set; }
//    }
//}
