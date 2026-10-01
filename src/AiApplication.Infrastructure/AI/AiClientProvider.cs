using AiApplication.Application.Abstractions;
using AiApplication.Application.Constants;
using System;
using System.Collections.Generic;

namespace AiApplication.Infrastructure.AI
{
    /// <summary>
    /// AI Client Provider。
    /// 负责统一管理所有 AI Client。
    /// </summary>
    public sealed class AiClientProvider : IAiClientProvider
    {
        /// <summary>
        /// 已注册的 AI Client。
        /// Key 为 Provider 名称。
        /// </summary>
        private readonly Dictionary<string, IAiClient> _clients;

        /// <summary>
        /// 初始化 AI Client Provider。
        /// </summary>
        /// <param name="openAiClient">
        /// OpenAI Client。
        /// </param>
        public AiClientProvider(OpenAiClient openAiClient)
        {
            if (openAiClient == null)
            {
                throw new ArgumentNullException(nameof(openAiClient));
            }

            _clients = new Dictionary<string, IAiClient>(StringComparer.OrdinalIgnoreCase);

            RegisterClient(AiProviderNames.OpenAI, openAiClient);
        }

        /// <summary>
        /// 根据 Provider 名称获取对应的 AI Client。
        /// </summary>
        /// <param name="provider">
        /// Provider 名称。
        /// </param>
        /// <returns>
        /// IAiClient。
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Provider 名称为空。
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Provider 不存在。
        /// </exception>
        public IAiClient GetClient(string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (!_clients.TryGetValue(provider, out IAiClient client))
            {
                throw new KeyNotFoundException($"未找到 Provider：{provider}。");
            }

            return client;
        }

        /// <summary>
        /// 注册 AI Client。
        /// </summary>
        /// <param name="provider">
        /// Provider 名称。
        /// </param>
        /// <param name="client">
        /// AI Client。
        /// </param>
        private void RegisterClient(string provider, IAiClient client)
        {
            if (string.IsNullOrWhiteSpace(provider))
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!_clients.TryAdd(provider, client))
            {
                throw new InvalidOperationException($"Provider '{provider}' 已经注册，不能重复注册。");
            }
        }
    }
}