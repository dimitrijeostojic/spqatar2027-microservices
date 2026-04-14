namespace Core;

public class Result<T> where T : class
{
    public T? Value { get; set; }
    public Error Error { get; set; }

    public bool IsSuccess => Error == Error.None;

    public Result(Error error)
    {
        Value = default;
        Error = error;
    }

    public Result(T? value)
    {
        Value = value;
        Error = Error.None;
    }

    public static Result<T> Success(T? Value) => new(Value);

    public static Result<T> Failure(Error Error) => new(Error);
}
