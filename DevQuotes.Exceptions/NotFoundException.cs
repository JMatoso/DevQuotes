namespace DevQuotes.Exceptions;

public class NotFoundException(string message) : ApplicationException(message) { }
