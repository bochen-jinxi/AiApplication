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
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            // 1. 绑定各厂商 Options
            services.Configure<OpenAiOptions>(configuration.GetSection("AI:OpenAI"));
            services.Configure<ClaudeOptions>(configuration.GetSection("AI:Claude"));
            services.Configure<DeepSeekOptions>(configuration.GetSection("AI:DeepSeek"));

            // 2. 注册 HttpClient（通过 IHttpClientFactory 管理生命周期，避免 DNS 老化）
            services.AddHttpClient<OpenAiClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<OpenAiOptions>>().Value;
                if (!string.IsNullOrWhiteSpace(options.BaseUrl))
                {
                    client.BaseAddress = new Uri(options.BaseUrl);
                }
                client.Timeout = TimeSpan.FromSeconds(60);
            });
            services.AddHttpClient<ClaudeClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<ClaudeOptions>>().Value;
                if (!string.IsNullOrWhiteSpace(options.BaseUrl))
                {
                    client.BaseAddress = new Uri(options.BaseUrl);
                }
                client.Timeout = TimeSpan.FromSeconds(60);
            });
            services.AddHttpClient<DeepSeekClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<DeepSeekOptions>>().Value;
                if (!string.IsNullOrWhiteSpace(options.BaseUrl))
                {
                    client.BaseAddress = new Uri(options.BaseUrl);
                }
                client.Timeout = TimeSpan.FromSeconds(60);
            });

            // 3. 注册各厂商响应解析器
            services.AddSingleton<ClaudeResponseParser>();
            services.AddSingleton<DeepSeekResponseParser>();

            // 4. 注册三个 AI 客户端为 IAiClient（多实现注册，AiClientProvider 通过 IEnumerable<IAiClient> 收集）
            services.AddSingleton<IAiClient>(sp =>
                sp.GetRequiredService<OpenAiClient>());
            services.AddSingleton<IAiClient>(sp =>
                sp.GetRequiredService<ClaudeClient>());
            services.AddSingleton<IAiClient>(sp =>
                sp.GetRequiredService<DeepSeekClient>());

            // 5. 注册 AI 客户端提供者（路由器）
            services.AddSingleton<IAiClientProvider, AiClientProvider>();

            // 6. 注册提示词构建器（默认通用构建器）

services
    .AddSingleton<
        IPromptTemplateRepository,
        MemoryPromptTemplateRepository>();

services
    .AddSingleton<
        IPromptBuilder,
        HydrologyPromptBuilder>();

services
    .AddSingleton<
        IPromptRenderer,
        PromptRenderer>();

          //  services.AddSingleton<IPromptBuilder, GeneralChatPromptBuilder>();

            //services.AddSingleton<IPromptTemplateRepository, FilePromptTemplateRepository>();
            //services.AddSingleton<IPromptRenderer, PromptRenderer>();
            services.AddDbContext<AppDbContext>();
            return services;
        }
    }
}
