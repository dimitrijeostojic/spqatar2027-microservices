namespace Core;

public class Result<T> where T : class
{
    public T? Value { get; set; }
    public Error Error { get; set; }
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



