namespace Core;

public class Result<T> where T : class
{
    public T? Value { get; }
    public Error Error { get; }
    public bool IsSuccess => Error == Error.None;

    public Result(T? value)
    {
        Value = value;
        Error = Error.None;
    }
    public Result(Error error)
    {
        Value = default;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
}
