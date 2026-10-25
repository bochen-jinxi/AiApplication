using System.Threading;
using System.Threading.Tasks;
using AiApplication.Application.Weather;

namespace AiApplication.Application.Abstractions.Weather
{
    /// <summary>
    /// 天气服务。
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// 获取天气。
        /// </summary>
        /// <param name="request">天气请求。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>天气结果。</returns>
        Task<WeatherResult> GetWeatherAsync(WeatherRequest request, CancellationToken cancellationToken = default);
    }
}