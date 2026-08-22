namespace AiApplication.Infrastructure.AI.OpenAI
{
    /// <summary>
    /// OpenAI 客户端配置。通常从 appsettings.json 的 "AI:OpenAI" 节点绑定。
    /// </summary>
    public sealed class OpenAiOptions
    {
        public const string SectionName = "AI:OpenAI";

        /// <summary>
        /// API 密钥。
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// API 基址。默认 https://api.openai.com/v1，可改为代理地址。
        /// </summary>
        public string BaseUrl { get; set; } = "https://api.openai.com/v1";

        /// <summary>
        /// 默认模型名称。
        /// </summary>
        public string DefaultModel { get; set; } = "gpt-4o-mini";

        /// <summary>
        /// 默认采样温度。
        /// </summary>
        public double DefaultTemperature { get; set; } = 0.7;

        /// <summary>
        /// 默认最大生成 Token 数。
        /// </summary>
        public int? DefaultMaxTokens { get; set; }
    }
}
