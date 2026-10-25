using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AiApplication.Application.Weather
{
    /// <summary>
    /// 天气查询请求。
    /// </summary>
    public sealed class WeatherRequest
    {
        [Description("城市名称")]
        [Required]
        public string City { get; set; }

         
    }
}
