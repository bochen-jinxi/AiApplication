using System.Collections.Generic;

namespace AiApplication.Application.Options
{
    /// <summary>
    /// AI 平台配置。
    /// </summary>
    public sealed class AiOptions
    {
        /// <summary>
        /// 默认 Provider。
        /// </summary>
        public string DefaultProvider { get; set; }

        /// <summary>
        /// 所有 Provider 配置。
        /// </summary>
        public Dictionary<string, AiProviderOptions> Providers { get; set; } = new Dictionary<string, AiProviderOptions>();
    }
}