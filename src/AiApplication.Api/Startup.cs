using AiApplication.Application.DependencyInjection;
using AiApplication.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            services.AddApplication();

            services.AddInfrastructure(Configuration);

            // 配置 MVC JSON 序列化使用同一份 JsonSerializerOptions
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = SharedJsonOptions.PropertyNamingPolicy;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = SharedJsonOptions.PropertyNameCaseInsensitive;
                    options.JsonSerializerOptions.WriteIndented = SharedJsonOptions.WriteIndented;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
