namespace AiApplication.Domain.Common
{
    /// <summary>
    /// 错误信息载体。用于在不抛异常的情况下传递失败信息。
    /// </summary>
    public sealed class Error
    {
        public string Code { get; }

        public string Message { get; }

        public Error(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new System.ArgumentException("错误代码不能为空。", nameof(code));
            }

            if (message == null)
            {
                throw new System.ArgumentNullException(nameof(message));
            }

            Code = code;
            Message = message;
        }

        public override string ToString() => $"[{Code}] {Message}";

        public static Error NotFound(string message) => new Error("NOT_FOUND", message);

        public static Error InvalidArgument(string message) => new Error("INVALID_ARGUMENT", message);

        public static Error Unauthorized(string message) => new Error("UNAUTHORIZED", message);

        public static Error ProviderError(string message) => new Error("PROVIDER_ERROR", message);

        public static Error Internal(string message) => new Error("INTERNAL", message);
    }
}
