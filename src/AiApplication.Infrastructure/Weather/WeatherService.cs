//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;
//using AiApplication.Application.Abstractions.Serialization;
//using AiApplication.Application.Abstractions.Weather;
//using AiApplication.Application.Serialization;
//using AiApplication.Application.Weather;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//namespace AiApplication.Infrastructure.Weather
//{
//    /// <summary>
//    /// 天气业务服务。
//    ///
//    /// 职责：
//    /// 1. 根据城市名称查询天气。
//    /// 2. 调用外部天气接口。
//    /// 3. 将外部接口返回的数据转换为统一的 WeatherResult。
//    ///
//    /// 说明：
//    /// 该类位于 Infrastructure 层，
//    /// 负责处理外部天气服务的具体调用细节。
//    /// Application 层只依赖 IWeatherService，
//    /// 不直接依赖 HTTP、JSON 组件或具体天气供应商。
//    /// </summary>
//    public sealed class WeatherService : IWeatherService
//    {
//        private readonly HttpClient _httpClient;
//        private readonly IJsonSerializer _jsonSerializer;
//        private readonly WeatherOptions _options;
//        private readonly ILogger<WeatherService> _logger;

//        /// <summary>
//        /// 初始化天气业务服务。
//        /// </summary>
//        /// <param name="httpClient">HTTP 客户端。</param>
//        /// <param name="jsonSerializer">JSON 序列化服务。</param>
//        /// <param name="options">天气服务配置。</param>
//        /// <param name="logger">日志服务。</param>
//        public WeatherService(
//            HttpClient httpClient,
//            IJsonSerializer jsonSerializer,
//            IOptions<WeatherOptions> options,
//            ILogger<WeatherService> logger)
//        {
//            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
//            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
//            if (options == null)
//            {
//                throw new ArgumentNullException(nameof(options));
//            }

//            if (options.Value == null)
//            {
//                throw new ArgumentException("WeatherOptions 不能为空。", nameof(options));
//            }

//            _options = options.Value;
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        }

//        /// <summary>
//        /// 根据城市名称获取天气信息。
//        /// </summary>
//        /// <param name="request">天气请求对象。</param>
//        /// <param name="cancellationToken">取消令牌。</param>
//        /// <returns>天气结果对象。</returns>
//        public async Task<WeatherResult> GetWeatherAsync(
//            WeatherRequest request,
//            CancellationToken cancellationToken)
//        {
//            if (request == null)
//            {
//                throw new ArgumentNullException(nameof(request));
//            }

//            if (string.IsNullOrWhiteSpace(request.City))
//            {
//                return CreateFailure(request.City, "城市名称不能为空。");
//            }

//            if (string.IsNullOrWhiteSpace(_options.BaseUrl))
//            {
//                return CreateFailure(request.City, "天气服务 BaseUrl 未配置。");
//            }

//            if (string.IsNullOrWhiteSpace(_options.ApiKey))
//            {
//                return CreateFailure(request.City, "天气服务 ApiKey 未配置。");
//            }

//            try
//            {
//                string requestUrl = BuildRequestUrl(request.City);

//                using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUrl))
//                {
//                    httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    httpRequest.Headers.Add("X-Api-Key", _options.ApiKey);

//                    using (var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken))
//                    {
//                        if (!httpResponse.IsSuccessStatusCode)
//                        {
//                            string errorContent = await httpResponse.Content.ReadAsStringAsync();

//                            _logger.LogWarning(
//                                "天气接口调用失败，City={0}, StatusCode={1}, Response={2}",
//                                request.City,
//                                httpResponse.StatusCode,
//                                errorContent);

//                            return CreateFailure(
//                                request.City,
//                                string.Format(
//                                    "天气接口调用失败，状态码：{0}。",
//                                    httpResponse.StatusCode));
//                        }

//                        string responseJson = await httpResponse.Content.ReadAsStringAsync();

//                        JsonDeserializeResult<WeatherApiResponse> deserializeResult =
//                            _jsonSerializer.TryDeserialize<WeatherApiResponse>(responseJson);

//                        if (!deserializeResult.Success)
//                        {
//                            return CreateFailure(
//                                request.City,
//                                string.Format("天气接口响应解析失败：{0}", deserializeResult.ErrorMessage));
//                        }

//                        WeatherApiResponse apiResponse = deserializeResult.Value;

//                        if (apiResponse == null)
//                        {
//                            return CreateFailure(request.City, "天气接口响应为空。");
//                        }

//                        if (!apiResponse.Success)
//                        {
//                            return CreateFailure(
//                                request.City,
//                                string.IsNullOrWhiteSpace(apiResponse.Message)
//                                    ? "天气接口返回失败。"
//                                    : apiResponse.Message);
//                        }

//                        return new WeatherResult
//                        {
//                            Success = true,
//                            City = string.IsNullOrWhiteSpace(apiResponse.City) ? request.City : apiResponse.City,
//                            Temperature = apiResponse.Temperature,
//                            Description = apiResponse.Description,
//                            ErrorMessage = null
//                        };
//                    }
//                }
//            }
//            catch (TaskCanceledException ex)
//            {
//                _logger.LogWarning(ex, "天气接口调用超时或被取消，City={0}", request.City);
//                return CreateFailure(request.City, "天气接口调用超时或被取消。");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "天气服务调用异常，City={0}", request.City);
//                return CreateFailure(request.City, string.Format("天气服务调用异常：{0}", ex.Message));
//            }
//        }

//        /// <summary>
//        /// 生成天气接口请求地址。
//        /// </summary>
//        /// <param name="city">城市名称。</param>
//        /// <returns>完整请求地址。</returns>
//        private string BuildRequestUrl(string city)
//        {
//            string baseUrl = _options.BaseUrl.TrimEnd('/');
//            string path = string.IsNullOrWhiteSpace(_options.QueryPath) ? "/weather" : _options.QueryPath.Trim();
//            if (!path.StartsWith("/", StringComparison.Ordinal))
//            {
//                path = "/" + path;
//            }

//            return string.Format(
//                "{0}{1}?city={2}&key={3}",
//                baseUrl,
//                path,
//                Uri.EscapeDataString(city),
//                Uri.EscapeDataString(_options.ApiKey));
//        }

//        /// <summary>
//        /// 创建失败结果。
//        /// </summary>
//        /// <param name="city">城市名称。</param>
//        /// <param name="errorMessage">错误信息。</param>
//        /// <returns>失败的天气结果。</returns>
//        private static WeatherResult CreateFailure(string city, string errorMessage)
//        {
//            return new WeatherResult
//            {
//                Success = false,
//                City = city ?? string.Empty,
//                Temperature = 0m,
//                Description = string.Empty,
//                ErrorMessage = errorMessage
//            };
//        }
//    }

//    /// <summary>
//    /// 天气服务配置。
//    ///
//    /// 说明：
//    /// 这个对象用于承载外部天气服务的配置，
//    /// 例如基础地址、接口路径、API Key 等。
//    /// 后续可以通过 Options Pattern 从 appsettings.json 绑定。
//    /// </summary>
//    public sealed class WeatherOptions
//    {
//        /// <summary>
//        /// 外部天气服务基础地址。
//        /// </summary>
//        public string BaseUrl { get; set; } = string.Empty;

//        /// <summary>
//        /// 外部天气服务接口路径。
//        /// </summary>
//        public string QueryPath { get; set; } = "/weather";

//        /// <summary>
//        /// 外部天气服务 API Key。
//        /// </summary>
//        public string ApiKey { get; set; } = string.Empty;
//    }

//    /// <summary>
//    /// 外部天气接口返回对象。
//    ///
//    /// 这个对象只用于 Infrastructure 层内部，
//    /// 不向上层暴露。
//    /// </summary>
//    internal sealed class WeatherApiResponse
//    {
//        /// <summary>
//        /// 接口是否成功。
//        /// </summary>
//        public bool Success { get; set; }

//        /// <summary>
//        /// 城市名称。
//        /// </summary>
//        public string City { get; set; }

//        /// <summary>
//        /// 温度。
//        /// </summary>
//        public decimal Temperature { get; set; }

//        /// <summary>
//        /// 天气描述。
//        /// </summary>
//        public string Description { get; set; }

//        /// <summary>
//        /// 错误信息。
//        /// </summary>
//        public string Message { get; set; }
//    }
//}