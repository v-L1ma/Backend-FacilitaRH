namespace FacilitaRhApi.Domain.Abstractions
{
    public record Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
        public static Result Failure(Error error) => new(false, error ?? throw new ArgumentNullException(nameof(error)));
        public static Result<T> Failure<T>(Error error) => Result<T>.Failure(error);

        public static implicit operator Result(Error error) => Failure(error);
    }

    public record Result<T> : Result
    {
        public T? Value { get; }

        private Result(T? value, bool isSuccess, Error? error) : base(isSuccess, error) => Value = value;

        private Result(T value) : base(true, null) => Value = value;
        private Result(Error error) : base(false, error) { }

        public static Result<T> Success(T value) => new(value);
        public static new Result<T> Failure(Error error) => new(error);

        public static implicit operator Result<T>(T value) => Success(value);

        public static implicit operator Result<T>(Error error) => Failure(error);
    }
}
