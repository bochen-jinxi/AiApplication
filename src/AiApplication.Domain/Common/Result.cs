namespace AiApplication.Domain.Common
{
    /// <summary>
    /// 操作结果。封装成功值或失败错误，避免通过异常控制流程。
    /// </summary>
    public sealed class Result
    {
        public bool IsSuccess { get; }

        public Error Error { get; }

        private Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);

        public static Result Failure(Error error)
        {
            if (error == null)
            {
                throw new System.ArgumentNullException(nameof(error));
            }

            return new Result(false, error);
        }
    }

    /// <summary>
    /// 携带返回值的操作结果。
    /// </summary>
    public sealed class Result<T>
    {
        public bool IsSuccess { get; }

        public T Value { get; }

        public Error Error { get; }

        private Result(bool isSuccess, T value, Error error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);

        public static Result<T> Failure(Error error)
        {
            if (error == null)
            {
                throw new System.ArgumentNullException(nameof(error));
            }

            return new Result<T>(false, default, error);
        }
    }
}
