//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using AiApplication.Application.Abstractions.Serialization;
//using AiApplication.Application.Abstractions.Tools;
//using AiApplication.Application.Abstractions.Weather;
//using AiApplication.Application.Serialization;
//using AiApplication.Application.Weather;

//namespace AiApplication.Application.Tools
//{
//    /// <summary>
//    /// 天气工具。
//    ///
//    /// 职责：
//    /// 1. 解析 LLM 返回的 ToolCall 参数。
//    /// 2. 调用 IWeatherService 获取天气数据。
//    /// 3. 将天气结果转换为统一的 ToolResult。
//    ///
//    /// 注意：
//    /// WeatherTool 不直接依赖 System.Text.Json，
//    /// 而是依赖 IJsonSerializer，
//    /// 这样以后更换 JSON 实现时，
//    /// WeatherTool 不需要修改。
//    /// </summary>
//    public sealed class WeatherTool : ITool
//    {
//        private readonly IWeatherService _weatherService;
//        private readonly IJsonSerializer _jsonSerializer;

//        /// <summary>
//        /// 初始化天气工具。
//        /// </summary>
//        /// <param name="weatherService">天气业务服务。</param>
//        /// <param name="jsonSerializer">JSON 序列化服务。</param>
//        public WeatherTool(IWeatherService weatherService, IJsonSerializer jsonSerializer)
//        {
//            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
//            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
//        }

//        /// <summary>
//        /// 工具名称。
//        /// </summary>
//        public string Name
//        {
//            get { return "Weather"; }
//        }

//        /// <summary>
//        /// 工具描述。
//        /// 该描述会提供给 LLM，用来判断是否需要调用该工具。
//        /// </summary>
//        public string Description
//        {
//            get { return "查询指定城市当前天气。"; }
//        }

//        /// <summary>
//        /// 执行天气查询工具。
//        /// </summary>
//        /// <param name="toolCall">LLM 返回的工具调用请求。</param>
//        /// <param name="cancellationToken">取消令牌。</param>
//        /// <returns>统一的工具执行结果。</returns>
//        public async Task<ToolResult> ExecuteAsync(ToolCall toolCall, CancellationToken cancellationToken)
//        {
//            if (toolCall == null)
//            {
//                throw new ArgumentNullException(nameof(toolCall));
//            }

//            JsonDeserializeResult<WeatherRequest> deserializeResult = _jsonSerializer.TryDeserialize<WeatherRequest>(toolCall.ArgumentsJson);

//            if (!deserializeResult.Success)
//            {
//                return new ToolResult
//                {
//                    Success = false,
//                    Content = string.Empty,
//                    ErrorMessage = "天气工具参数解析失败：" + deserializeResult.ErrorMessage
//                };
//            }

//            WeatherRequest request = deserializeResult.Value;

//            WeatherResult weatherResult = await _weatherService.GetWeatherAsync(request, cancellationToken);

//            if (!weatherResult.Success)
//            {
//                return new ToolResult
//                {
//                    Success = false,
//                    Content = string.Empty,
//                    ErrorMessage = weatherResult.ErrorMessage
//                };
//            }

//            return new ToolResult
//            {
//                Success = true,
//                Content = string.Format("{0}当前天气：{1}，温度：{2}℃。", weatherResult.City, weatherResult.Description, weatherResult.Temperature),
//                ErrorMessage = null
//            };
//        }
//    }
//}