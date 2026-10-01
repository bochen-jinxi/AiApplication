//using System;
//using AiApplication.Application.Abstractions.AI;
//using AiApplication.Application.Abstractions.Prompt;
//using AiApplication.Application.Abstractions.Serialization;
//using AiApplication.Application.Abstractions.Tools;
//using AiApplication.Application.Abstractions.Weather;
//using AiApplication.Application.Tools;
//using AiApplication.Infrastructure.AI;
//using AiApplication.Infrastructure.AI.Claude;
//using AiApplication.Infrastructure.AI.DeepSeek;
//using AiApplication.Infrastructure.AI.OpenAI;
//using AiApplication.Infrastructure.Persistence;
//using AiApplication.Infrastructure.Prompt.Builders;
//using AiApplication.Infrastructure.Prompt.Rendering;
//using AiApplication.Infrastructure.Prompt.Repository;
//using AiApplication.Infrastructure.Serialization;
//using AiApplication.Infrastructure.Tools;
//using AiApplication.Infrastructure.Weather;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;

//namespace AiApplication.Infrastructure.DependencyInjection
//{
//    /// <summary>
//    /// Infrastructure 层服务注册扩展。
//    /// 负责注册 AI 客户端、提示词构建器等基础设施服务，
//    /// 并从 <see cref="IConfiguration"/> 绑定各厂商 Options。
//    /// </summary>
//    public static class InfrastructureServiceExtensions
//    {
//        /// <summary>
//        /// 注册 Infrastructure 层服务。
//        /// </summary>
//        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
//        {
//             if (services == null)
//            {
//                throw new ArgumentNullException(nameof(services));
//            }

//            if (configuration == null)
//            {
//                throw new ArgumentNullException(nameof(configuration));
//            }

//            services.AddJsonSerializer(configuration);
//            services.AddWeatherServices(configuration);
//            services.AddToolServices();


//            // 1. 绑定各厂商 Options
//            //    同时将 IOptions<T>.Value 注册为 T 单例，
//            //    以便通过构造函数直接注入 T（如 OpenAiClient(OpenAiOptions options)）。
//            services.Configure<OpenAiOptions>(configuration.GetSection("AI:OpenAI"));
//            services.AddSingleton(sp => sp.GetRequiredService<IOptions<OpenAiOptions>>().Value);

           

//            // 2. 注册 HttpClient（通过 IHttpClientFactory 管理生命周期，避免 DNS 老化）
//            services.AddHttpClient<OpenAiClient>((sp, client) =>
//            {
//                var options = sp.GetRequiredService<IOptions<OpenAiOptions>>().Value;
//                if (!string.IsNullOrWhiteSpace(options.BaseUrl))
//                {
//                    client.BaseAddress = new Uri(options.BaseUrl);
//                }
//                client.Timeout = TimeSpan.FromSeconds(60);
//            });
           


//            // 4. 注册三个 AI 客户端为 IAiClient（多实现注册，AiClientProvider 通过 IEnumerable<IAiClient> 收集）
//            services.AddSingleton<IAiClient>(sp =>
//                sp.GetRequiredService<OpenAiClient>());
          

//            // 5. 注册 AI 客户端提供者（路由器）
//            services.AddSingleton<IAiClientProvider, AiClientProvider>();

//            // 6. 注册提示词构建器（默认通用构建器）

//            services.AddSingleton<IPromptTemplateRepository, MemoryPromptTemplateRepository>();

//            services.AddSingleton<IPromptBuilder, HydrologyPromptBuilder>();

//            services.AddSingleton<IPromptRenderer, PromptRenderer>();

//            services.AddSingleton<ITool, Application.Tools.WeatherTool>();

//            services.AddSingleton<ITool, SearchTool>();

//            services.AddSingleton<ITool, DatabaseTool>();
                    
//            services.AddDbContext<AppDbContext>();

//            services.AddSingleton<IToolRegistry, ToolRegistry>();

//            services.AddScoped<IToolExecutor,ToolExecutor>();

//            services.AddSingleton<ITool, Application.Tools.WeatherTool>();

//            services.AddSingleton<ITool,SearchTool>();

//            services.AddSingleton<ITool,DatabaseTool>();
//        return services;
//        }

//         /// <summary>
//        /// 注册 JSON 序列化服务。
//        /// </summary>
//        /// <param name="services">服务集合。</param>
//        /// <param name="configuration">应用配置。</param>
//        /// <returns>服务集合。</returns>
//        private static IServiceCollection AddJsonSerializer(
//            this IServiceCollection services,
//            IConfiguration configuration)
//        {
//            services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();

//            return services;
//        }

//        /// <summary>
//        /// 注册天气相关服务。
//        /// </summary>
//        /// <param name="services">服务集合。</param>
//        /// <param name="configuration">应用配置。</param>
//        /// <returns>服务集合。</returns>
//        private static IServiceCollection AddWeatherServices(
//            this IServiceCollection services,
//            IConfiguration configuration)
//        {
//            services.Configure<WeatherOptions>(configuration.GetSection("Weather"));

//            services.AddHttpClient<IWeatherService, WeatherService>();

//            return services;
//        }

//        /// <summary>
//        /// 注册工具相关服务。
//        /// </summary>
//        /// <param name="services">服务集合。</param>
//        /// <returns>服务集合。</returns>
//        private static IServiceCollection AddToolServices(
//            this IServiceCollection services)
//        {
//            services.AddSingleton<IToolRegistry, ToolRegistry>();
//            services.AddScoped<IToolExecutor, ToolExecutor>();
//            services.AddSingleton<ITool, WeatherTool>();

//            return services;
//        }
//    }
//}
