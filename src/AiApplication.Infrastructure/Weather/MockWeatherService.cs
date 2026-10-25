using System;
using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Abstractions.Weather;
using AiApplication.Application.Weather;

namespace AiApplication.Infrastructure.Weather
{
    /// <summary>
    /// 模拟天气服务。
    /// </summary>
    public sealed class MockWeatherService : IWeatherService
    {
        /// <summary>
        /// 获取天气。
        /// </summary>
        /// <param name="request">天气请求。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>天气结果。</returns>
        public Task<WeatherResult> GetWeatherAsync(WeatherRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            WeatherResult result = new WeatherResult
            {
                City = request.City,
                Weather = "晴",
                Temperature = 31,
                Humidity = 65,
                WindDirection = "东南风",
                WindPower = "3级",
                PublishTime = DateTime.Now
            };

            return Task.FromResult(result);
        }
    }
}