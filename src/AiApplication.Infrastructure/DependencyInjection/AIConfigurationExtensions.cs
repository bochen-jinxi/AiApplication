using AiApplication.Application.Abstractions;
using AiApplication.Application.Options;
using AiApplication.Infrastructure.AI;
using AiApplication.Infrastructure.Http;
using AiApplication.Infrastructure.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiApplication.Infrastructure.DependencyInjection
{
    /// <summary>
    /// AI 平台依赖注入扩展。
    /// </summary>
    public static class AIConfigurationExtensions
    {
        /// <summary>
        /// AI 配置节名称。
        /// </summary>
        private const string AiSectionName = "AI";
        /// <summary>
        /// 注册 AI 平台核心服务。
        /// </summary>
        /// <param name="services">
        /// IServiceCollection。
        /// </param>
        /// <param name="configuration">
        /// IConfiguration。
        /// </param>
        /// <returns>
        /// IServiceCollection。
        /// </returns>
        public static IServiceCollection AddAiCore(this IServiceCollection services, IConfiguration configuration)
        {
            // 注册 AI Options
            RegisterOptions(services,configuration);

            // 注册序列化器
            RegisterSerializer(services);

            // 注册 HTTP 传输层
            RegisterHttp(services);

            // 注册 AI Provider
            RegisterProviders(services);

            // 注册 Tool
           // RegisterTools(services);

            return services;
        }

        /// <summary>
        /// 注册 AI 配置。
        /// </summary>
        /// <param name="services">
        /// 服务集合。
        /// </param>
        /// <param name="configuration">
        /// IConfiguration。
        /// </param>
        private static void RegisterOptions( IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AiOptions>(configuration.GetSection(AiSectionName));
        }

        /// <summary>
        /// 注册 JSON 序列化器。
        /// </summary>
        /// <param name="services">
        /// 服务集合。
        /// </param>
        private static void RegisterSerializer(IServiceCollection services)
        {          
            services.AddSingleton<IAiSerializer, SystemTextJsonSerializer>();
        }
        /// <summary>
        /// 注册 HTTP 传输层。
        /// </summary>
        /// <param name="services">
        /// 服务集合。
        /// </param>
        private static void RegisterHttp(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddSingleton<IAiHttpTransport, AiHttpTransport>();
        }
        /// <summary>
        /// 注册 AI Provider。
        /// </summary>
        /// <param name="services">
        /// 服务集合。
        /// </param>
        private static void RegisterProviders(IServiceCollection services)
        {
            services.AddSingleton<OpenAiClient>();

            services.AddSingleton<IAiClientProvider, AiClientProvider>();
        }

        ///// <summary>
        ///// 注册 Tool。
        ///// </summary>
        ///// <param name="services">
        ///// 服务集合。
        ///// </param>
        //private static void RegisterTools(IServiceCollection services)
        //{
        //    services.AddSingleton<IToolExecutor, ToolExecutor>();
        //}
    }
}