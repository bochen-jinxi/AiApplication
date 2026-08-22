namespace AiApplication.Application.Abstractions.AI
{
    /// <summary>
    /// AI 响应解析器抽象。将各厂商返回的原始 JSON 字符串解析为统一的 <see cref="AiCompletion"/>。
    /// 不同厂商的响应结构差异较大，因此每个客户端实现都对应一个独立的解析器。
    /// </summary>
    public interface IAiResponseParser
    {
        /// <summary>
        /// 解析原始响应字符串。
        /// </summary>
        AiCompletion Parse(string rawResponse);
    }
}
