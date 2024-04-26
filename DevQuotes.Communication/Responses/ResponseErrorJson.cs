using System.Text.Json.Serialization;

namespace DevQuotes.Communication.Responses;

public class ResponseErrorJson(string message, IEnumerable<object> errors)
{
    public string Message { get; set; } = message;

    public IEnumerable<object> Errors { get; set; } = errors;
}