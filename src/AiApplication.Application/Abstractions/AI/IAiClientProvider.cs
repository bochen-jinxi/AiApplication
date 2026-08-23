using System;
using AiApplication.Domain.AI;

namespace AiApplication.Application.Abstractions.AI
{
    /// <summary>
    /// AI 客户端提供者。根据 <see cref="AiProvider"/> 路由到具体的 <see cref="IAiClient"/> 实现。
    /// 这是 ChatService 与具体 AI 厂商之间的"调度器"，使上层无需关心当前使用的是哪家模型。
    /// </summary>
    public interface IAiClientProvider
    {
        /// <summary>
        /// 根据提供商枚举获取对应的客户端。
        /// </summary>
        /// <param name="provider">目标提供商。</param>
        /// <returns>对应的 <see cref="IAiClient"/> 实例。</returns>
        /// <exception cref="NotSupportedException">当传入的 <paramref name="provider"/> 未注册时抛出。</exception>
        IAiClient GetClient(AiProvider provider); 

     
    }
}
