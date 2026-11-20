namespace Gymy.BusinessLogic.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public string? Errorkey { get; }

    protected Result(bool isSuccess, string? error, string? errorkey = null)
    {
        if (isSuccess && error is not null)
            throw new InvalidOperationException("Success result cannot have an error.");

        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("Failure result must have an error.");

        IsSuccess = isSuccess;
        Error = error;
        Errorkey = errorkey;
    }

    public static Result Success() => new(true, null);

    public static Result Failure(string error, string? errorkey = null) =>
        new(false, error, errorkey);
}

public sealed class Result<T> : Result
{
    private Result(bool isSuccess, T? value, string? error, string? errorkey = null)
        : base(isSuccess, error, errorkey)
    {
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T value) => new(true, value, null, null);

    public static new Result<T> Failure(string error, string? errorkey = null) =>
        new(false, default, error, errorkey);
}