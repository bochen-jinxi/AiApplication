using AiApplication.Application.Enums;

namespace AiApplication.Application.Abstractions
{
    /// <summary>
    /// AI Client Provider。
    /// </summary>
    public interface IAiClientProvider
    {
        /// <summary>
        /// 获取 AI Client。
        /// </summary>
        /// <param name="provider">
        /// AI Provider。
        /// </param>
        /// <returns>
        /// IAiClient。
        /// </returns>
        IAiClient GetClient(string provider);
    }
}