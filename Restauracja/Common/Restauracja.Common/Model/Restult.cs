namespace Restauracja.Common.Model;

public class Result
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public static Result Fail(string message = "") =>
        new()
        {
            IsSuccess = false,
            Message = message
        };

    public static Result Success() =>
        new()
        {
            IsSuccess = true
        };


    public static Result Success<T>(T data) =>
        new Result<T>
        {
            IsSuccess = true,
            Data = data
        };
}

public class Result<T> : Result 
{
    public T Data { get; set; }
}