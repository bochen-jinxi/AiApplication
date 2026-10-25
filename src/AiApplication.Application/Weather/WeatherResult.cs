using System;

namespace AiApplication.Application.Weather
{
    /// <summary>
    /// 天气查询结果。
    /// </summary>
    public sealed class WeatherResult
    {
        /// <summary>
        /// 城市。
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 天气。
        /// </summary>
        public string Weather { get; set; }

        /// <summary>
        /// 温度（℃）。
        /// </summary>
        public decimal Temperature { get; set; }

        /// <summary>
        /// 湿度（%）。
        /// </summary>
        public int Humidity { get; set; }

        /// <summary>
        /// 风向。
        /// </summary>
        public string WindDirection { get; set; }

        /// <summary>
        /// 风力。
        /// </summary>
        public string WindPower { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public DateTime PublishTime { get; set; }
    }
}