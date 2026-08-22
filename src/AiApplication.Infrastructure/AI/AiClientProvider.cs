using System;
using System.Collections.Generic;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.AI
{
    /// <summary>
    /// AI 客户端提供者实现。维护一个 <see cref="AiProvider"/> → <see cref="IAiClient"/> 的映射表，
    /// 在构造时注入所有已注册的客户端实例。
    /// </summary>
    public sealed class AiClientProvider : IAiClientProvider
    {
        private readonly IReadOnlyDictionary<AiProvider, IAiClient> _clients;

        public AiClientProvider(IEnumerable<IAiClient> clients)
        {
            if (clients == null)
            {
                throw new ArgumentNullException(nameof(clients));
            }

            var dict = new Dictionary<AiProvider, IAiClient>();
            foreach (var client in clients)
            {
                if (client == null)
                {
                    continue;
                }

                // 后注册的同提供商客户端覆盖先注册的，便于测试替换。
                dict[client.Provider] = client;
            }

            _clients = dict;
        }

        public IAiClient GetClient(AiProvider provider)
        {
            if (_clients.TryGetValue(provider, out var client))
            {
                return client;
            }

            throw new NotSupportedException($"未注册 AI 提供商: {provider}。请在 DI 容器中注册对应的 IAiClient 实现。");
        }

        public IAiClient GetClient(AiModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return GetClient(model.Provider);
        }
    }
}
