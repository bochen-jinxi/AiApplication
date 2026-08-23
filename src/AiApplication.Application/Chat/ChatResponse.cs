using AiApplication.Domain.AI;
using AiApplication.Domain.Common;

namespace AiApplication.Application.Chat
{
    /// <summary>
    /// 聊天响应。封装 AI 回复内容、Token 用量以及可能的错误。
    /// </summary>
    public sealed class ChatResponse
    {

 public   string Content
    {
        get;
        set;
    }

    public   string Model
    {
        get;
        set;
    }

    public int PromptTokens
    {
        get;
        set;
    }

    public int CompletionTokens
    {
        get;
        set;
    }

    public string? FinishReason
    {
        get;
        set;
    }


    public string Provider { get; set; }

    public int TotalTokens { get; set; }

        public bool IsSuccess { get; }

     

        public TokenUsage Usage { get; }

        public Error Error { get; }

        private ChatResponse(bool isSuccess, string content, TokenUsage usage, Error error)
        {
            IsSuccess = isSuccess;
            Content = content;
            Usage = usage;
            Error = error;
        }

        public static ChatResponse Success(string content, TokenUsage usage) =>
            new ChatResponse(true, content, usage ?? TokenUsage.Empty, null);

        public static ChatResponse Failure(Error error) =>
            new ChatResponse(false, null, null, error ?? Error.Internal("未知错误。"));
    }
}
