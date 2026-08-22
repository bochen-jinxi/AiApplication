using System;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Application.Abstractions.Prompt;
using AiApplication.Infrastructure.AI;
using AiApplication.Infrastructure.AI.Claude;
using AiApplication.Infrastructure.AI.DeepSeek;
using AiApplication.Infrastructure.AI.OpenAI;
using AiApplication.Infrastructure.Persistence;
using AiApplication.Infrastructure.Prompt.Builders;
using AiApplication.Infrastructure.Prompt.Rendering;
using AiApplication.Infrastructure.Prompt.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AiApplication.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Infrastructure 层服务注册扩展。
    /// 负责注册 AI 客户端、提示词构建器等基础设施服务，
    /// 并从 <see cref="IConfiguration"/> 绑定各厂商 Options。
    /// </summary>
    public static class InfrastructureServiceExtensions
    {
        /// <summary>
        /// 注册 Infrastructure 层服务。
        /// </summary>
        /// <param name="services">服务集合。</param>
        /// <param name="configuration">应用配置，用于读取 "AI:OpenAI" / "AI:Claude" / "AI:DeepSeek" 节点。</param>
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            // 1. 绑定并注册 Options（同时注册 IOptions<T> 与 T 单例，便于客户端构造函数直接注入 T）
            RegisterOptions<OpenAiOptions>(services, configuration, OpenAiOptions.SectionName);
            RegisterOptions<ClaudeOptions>(services, configuration, ClaudeOptions.SectionName);
            RegisterOptions<DeepSeekOptions>(services, configuration, DeepSeekOptions.SectionName);

            // 2. 注册 HttpClient（通过 IHttpClientFactory 管理生命周期，避免 DNS 老化）
            services.AddHttpClient<OpenAiClient>(ConfigureOpenAiHttpClient);
            services.AddHttpClient<ClaudeClient>(ConfigureClaudeHttpClient);
            services.AddHttpClient<DeepSeekClient>(ConfigureDeepSeekHttpClient);

            // 3. 注册各厂商响应解析器
            services.AddSingleton<OpenAiResponseParser>();
            services.AddSingleton<ClaudeResponseParser>();
            services.AddSingleton<DeepSeekResponseParser>();

            services.AddSingleton<IAiClient, OpenAiClient>();
            services.AddSingleton<IAiClient, ClaudeClient>();
            services.AddSingleton<IAiClient, DeepSeekClient>(); 

            // 5. 注册 AI 客户端提供者（路由器）
            services.AddSingleton<IAiClientProvider, AiClientProvider>();

            // 6. 注册提示词构建器（默认通用构建器）
            services.AddSingleton<IPromptBuilder, GeneralChatPromptBuilder>();


services.AddSingleton<
    IPromptTemplateRepository,
    FilePromptTemplateRepository>();

services.AddSingleton<
    IPromptRenderer,
    PromptRenderer>();
    services.AddDbContext<AppDbContext>();
            return services;
        }

        /// <summary>
        /// 同时注册 IOptions&lt;T&gt;（支持 Configure 变更通知）与 T 单例（便于直接注入）。
        /// </summary>
        private static void RegisterOptions<T>(
            IServiceCollection services,
            IConfiguration configuration,
            string sectionName)
            where T : class, new()
        {
            // IOptions<T> 标准注册
            services.Configure<T>(configuration.GetSection(sectionName));

            // T 单例：从 IOptions<T>.Value 解析，供客户端构造函数直接注入
            services.AddSingleton<T>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<T>>();
                return options.Value ?? new T();
            });
        }

        private static void ConfigureOpenAiHttpClient(
            IServiceProvider sp,
            System.Net.Http.HttpClient client)
        {
            var options = sp.GetRequiredService<OpenAiOptions>();
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            }
        }

        private static void ConfigureClaudeHttpClient(
            IServiceProvider sp,
            System.Net.Http.HttpClient client)
        {
            var options = sp.GetRequiredService<ClaudeOptions>();
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            }
        }

        private static void ConfigureDeepSeekHttpClient(
            IServiceProvider sp,
            System.Net.Http.HttpClient client)
        {
            var options = sp.GetRequiredService<DeepSeekOptions>();
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            }
        }
    }
}
