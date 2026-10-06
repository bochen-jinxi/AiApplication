using AiApplication.Application.Abstractions;
using AiApplication.Infrastructure.AI;
using AiApplication.Infrastructure.DependencyInjection;
using AiApplication.Infrastructure.Http;
using AiApplication.Infrastructure.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Net.Http;
using System.Text.Json;

namespace AiApplication.Api
{
    public class Startup
    {
        private static readonly JsonSerializerOptions SharedJsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 注册共享的 JsonSerializerOptions，供 OpenAiResponseParser 等服务注入
            services.AddSingleton(SharedJsonOptions);


            services.AddAiCore(Configuration);
            services.AddHttpClient();

            services.AddSingleton<IAiHttpTransport, AiHttpTransport>();
            services.AddSingleton<IAiSerializer, SystemTextJsonSerializer>();

            // 注册 OpenAI Client
           // services.AddSingleton<OpenAiClient>();
            services.AddSingleton<IAiClient,OpenAiClient>();
            // 注册 IAiClient
            //services.AddSingleton<IAiClient>(sp => sp.GetRequiredService<OpenAiClient>());

            // 注册 Client Provider
            services.AddSingleton<IAiClientProvider, AiClientProvider>();

            //services.AddScoped<IAiClientProvider, AiClientProvider>();
            //services.AddScoped<IAiClient, OpenAiClient>();
            //services.AddScoped<IAiHttpTransport, AiHttpTransport>();



            //services.AddApplication();

            //services.AddInfrastructure(Configuration);

            // 配置 MVC JSON 序列化使用同一份 JsonSerializerOptions
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = SharedJsonOptions.PropertyNamingPolicy;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = SharedJsonOptions.PropertyNameCaseInsensitive;
                    options.JsonSerializerOptions.WriteIndented = SharedJsonOptions.WriteIndented;
                });

            // 配置 Swagger（OpenAPI 文档生成 + 交互式 UI）
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AiApplication API",
                    Version = "v1",
                    Description = "AI 应用 API 文档"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // 启用 Swagger 中间件与交互式 UI（仅开发环境）
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AiApplication API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
