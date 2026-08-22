namespace AiApplication.Infrastructure.AI.Claude
{
    /// <summary>
    /// Anthropic Claude 客户端配置。从 appsettings.json 的 "AI:Claude" 节点绑定。
    /// </summary>
    public sealed class ClaudeOptions
    {
        public const string SectionName = "AI:Claude";

        public string ApiKey { get; set; }

        public string BaseUrl { get; set; } = "https://api.anthropic.com/v1";

        public string DefaultModel { get; set; } = "claude-3-5-sonnet-20240620";

        public double DefaultTemperature { get; set; } = 0.7;

        public int? DefaultMaxTokens { get; set; } = 1024;

        /// <summary>
        /// Anthropic API 版本号，对应 x-api-key / anthropic-version 头。
        /// </summary>
        public string ApiVersion { get; set; } = "2023-06-01";
    }
}
