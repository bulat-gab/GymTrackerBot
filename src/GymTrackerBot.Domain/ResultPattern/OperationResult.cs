namespace GymTrackerBot.Domain.ResultPattern;

public class OperationResult<T>
{
    public bool Success { get; }
    public string? Error { get; }
    public T? Data { get; }

    private OperationResult(bool success, T? data, string? error)
    {
        Success = success;
        Data = data;
        Error = error;
    }

    public static OperationResult<T> Ok(T data) => new(true, data, null);
    public static OperationResult<T> Fail(string error) => new(false, default, error);
}
