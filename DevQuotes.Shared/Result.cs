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

public class ResultObject<TClass>(TClass? data, string message) where TClass : class
{
    public TClass? Data { get; private set; } = data;
    public string Message { get; private set; } = message;

    public bool Succeeded => Data is not null && string.IsNullOrEmpty(Message);

    public static ResultObject<TClass> Success(TClass data)
    {
        return new ResultObject<TClass>(data, string.Empty);
    }

    public static ResultObject<TClass> Fail(string message)
    {
        return new ResultObject<TClass>(default, message);
    }
}