namespace DevQuotes.Shared;

public class Result(bool succeeded, string? message = null, object? data = null)
{
    public bool Succeeded { get; private set; } = succeeded;
    public string? Message { get; private set; } = message;
    public object? Data { get; private set; } = data;

    public static Result Success(string? message = null, object? data = null)
    {
        return new Result(true, message, data);
    }

    public static Result Fail(string? message = null, object? data = null)
    {
        return new Result(false, message, data);
    }
}