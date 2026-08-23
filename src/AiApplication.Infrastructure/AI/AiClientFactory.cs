using System;
using System.Collections.Generic;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.AI
{
    /// <summary>
    /// AI 客户端工厂。提供按需创建已配置好的 <see cref="IAiClient"/> 实例的能力，
    /// 并可一次性组装出 <see cref="IAiClientProvider"/>。
    /// </summary>
    public static class AiClientFactory
    {
        /// <summary>
        /// 创建包含 OpenAI / Claude / DeepSeek 三个客户端的 <see cref="IAiClientProvider"/>。
        /// </summary>
        public static IAiClientProvider CreateProvider(
            OpenAI.OpenAiOptions openAiOptions,
            Claude.ClaudeOptions claudeOptions,
            DeepSeek.DeepSeekOptions deepSeekOptions,
            System.Text.Json.JsonSerializerOptions jsonOptions = null)
        {
            if (openAiOptions == null) throw new ArgumentNullException(nameof(openAiOptions));
            if (claudeOptions == null) throw new ArgumentNullException(nameof(claudeOptions));
            if (deepSeekOptions == null) throw new ArgumentNullException(nameof(deepSeekOptions));

            var claudeParser = new Claude.ClaudeResponseParser();
            var deepSeekParser = new DeepSeek.DeepSeekResponseParser();

            var clients = new IAiClient[]
            {
                new OpenAI.OpenAiClient(CreateHttpClient(openAiOptions.BaseUrl), openAiOptions, jsonOptions),
                new Claude.ClaudeClient(CreateHttpClient(claudeOptions.BaseUrl), claudeOptions, claudeParser),
                new DeepSeek.DeepSeekClient(CreateHttpClient(deepSeekOptions.BaseUrl), deepSeekOptions, deepSeekParser)
            };

            return new AiClientProvider(clients);
        }

        /// <summary>
        /// 仅创建指定提供商的客户端。适用于只使用单一厂商的场景。
        /// </summary>
        public static IAiClient CreateClient(
            AiProvider provider,
            object options,
            System.Text.Json.JsonSerializerOptions jsonOptions = null)
        {
            switch (provider)
            {
                case AiProvider.OpenAI:
                    return new OpenAI.OpenAiClient(
                        CreateHttpClient(((OpenAI.OpenAiOptions)options).BaseUrl),
                        (OpenAI.OpenAiOptions)options,
                        jsonOptions);

                case AiProvider.Claude:
                    return new Claude.ClaudeClient(
                        CreateHttpClient(((Claude.ClaudeOptions)options).BaseUrl),
                        (Claude.ClaudeOptions)options,
                        new Claude.ClaudeResponseParser());

                case AiProvider.DeepSeek:
                    return new DeepSeek.DeepSeekClient(
                        CreateHttpClient(((DeepSeek.DeepSeekOptions)options).BaseUrl),
                        (DeepSeek.DeepSeekOptions)options,
                        new DeepSeek.DeepSeekResponseParser());

                default:
                    throw new NotSupportedException($"不支持的 AI 提供商: {provider}。");
            }
        }

        private static System.Net.Http.HttpClient CreateHttpClient(string baseUrl)
        {
            var client = new System.Net.Http.HttpClient();
            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                client.BaseAddress = new Uri(baseUrl);
            }

            return client;
        }
    }
}
