using System;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions.Tools;
using AiApplication.Application.Abstractions.Weather;
using AiApplication.Application.Tools;
using AiApplication.Application.Weather;

namespace AiApplication.Infrastructure.Tools.Weather
{
    /// <summary>
    /// 天气 Tool。
    /// </summary>
    public sealed class WeatherTool : ITool
    {
        private readonly IToolParameterBinder _parameterBinder;
        private readonly IWeatherService _weatherService;

        /// <summary>
        /// 初始化天气 Tool。
        /// </summary>
        /// <param name="parameterBinder">参数绑定器。</param>
        /// <param name="weatherService">天气服务。</param>
        public WeatherTool(IToolParameterBinder parameterBinder, IWeatherService weatherService)
        {
            _parameterBinder = parameterBinder ?? throw new ArgumentNullException(nameof(parameterBinder));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        /// <summary>
        /// Tool 名称。
        /// </summary>
        public string Name => "weather";

        /// <summary>
        /// Tool 描述。
        /// </summary>
        public string Description => "获取指定城市天气。";

        /// <summary>
        /// 执行 Tool。
        /// </summary>
        /// <param name="context">执行上下文。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>执行结果。</returns>
        public async Task<ToolResult> ExecuteAsync(ToolExecutionContext context, CancellationToken cancellationToken = default)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            WeatherRequest request = _parameterBinder.Bind<WeatherRequest>(context);

            WeatherResult weather = await _weatherService.GetWeatherAsync(request, cancellationToken);

            string content = $"{weather.City}，{weather.Weather}，{weather.Temperature}℃，湿度{weather.Humidity}%，{weather.WindDirection}{weather.WindPower}。";

            return ToolResult.Ok(content);
        }
    }
}