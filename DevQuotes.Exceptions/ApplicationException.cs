namespace DevQuotes.Exceptions;

public sealed class ApplicationException : Exception
{
    public ApplicationException(string message = "A few validation errors ocurred.", ExceptionTypes exceptionType = ExceptionTypes.BadRequest) 
        : base(message)
    {
        Source = exceptionType.ToString();
    }

    public void AddPropertyError(object key, object args)
    {
        Data.Add(key, args);
    }
}
