namespace DevQuotes.Exceptions;

public class ErrorOnValidationException(string message) : ApplicationException(message) { }
