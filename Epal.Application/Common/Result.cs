namespace Epal.Application.Common;

public class Result
{
    public bool Success { get; set; }
    
    public string? Error { get; set; }

    public static Result Ok() => new Result { Success = true };
    public static Result Fail(string error) => new Result { Error = error };
}

public class Result<T>
{
    public bool Success { get; set; }
    
    public T? Data { get; set; }
    
    public string? Error { get; set; }

    public static Result<T> Ok(T data) => new Result<T> { Success = true, Data = data };
    public static Result<T> Fail(string error) => new Result<T> { Error = error };
}