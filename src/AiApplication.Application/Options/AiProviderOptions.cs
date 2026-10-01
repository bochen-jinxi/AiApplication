namespace AiApplication.Application.Options
{
    /// <summary>
    /// AI Provider 配置。
    /// </summary>
    public sealed class AiProviderOptions
    {
        /// <summary>
        /// API 地址。
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// API Key。
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 默认模型。
        /// </summary>
        public string Model { get; set; }

        public bool Enabled { get; set; } = true;

    }
}