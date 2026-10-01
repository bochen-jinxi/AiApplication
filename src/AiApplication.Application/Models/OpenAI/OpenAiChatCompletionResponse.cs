using AiApplication.Application.Models.OpenAI;
using System.Collections.Generic;

public sealed class OpenAiChatCompletionResponse
{
    /// <summary>
    /// 对象类型。
    /// </summary>
    public string Object { get; set; }

    /// <summary>
    /// 唯一标识。
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 创建时间。
    /// </summary>
    public long Created { get; set; }

    /// <summary>
    /// 模型名称。
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// 返回结果。
    /// </summary>
    public List<OpenAiChoice> Choices { get; set; }

    /// <summary>
    /// Token 使用情况。
    /// </summary>
    public OpenAiUsage Usage { get; set; }
}