//using AiApplication.Application.Chat;
//using AiApplication.Application.Mcp;
//using AiApplication.Application.Rag;
//using Microsoft.Extensions.DependencyInjection;

//namespace AiApplication.Application.DependencyInjection
//{
//    /// <summary>
//    /// Application 层服务注册扩展。
//    /// 负责注册应用服务（ChatService 等），不关心 Infrastructure 层的具体实现。
//    /// </summary>
//    public static class ApplicationServiceExtensions
//    {
//        /// <summary>
//        /// 注册 Application 层服务。
//        /// </summary>
//        /// <remarks>
//        /// ChatService 依赖 IAiClientProvider 与 IPromptBuilder，这两者由 Infrastructure 层
//        /// 的 <c>AddInfrastructure</c> 注册；IRagService / IMcpService 为可选依赖，
//        /// 未注册时 ChatService 以 null 容忍方式跳过对应功能。
//        /// </remarks>
//        public static IServiceCollection AddApplication(this IServiceCollection services)
//        {
//            services.AddScoped<IChatService, ChatService>();
//            services.AddScoped<IRagService, RagService>();
//            services.AddScoped<IMcpService, McpService>();
//            return services;
//        }
//    }
//}
