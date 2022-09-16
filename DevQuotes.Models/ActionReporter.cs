﻿using System.Text.Json.Serialization;

namespace DevQuotes.Models;

public class ActionReporter
{
    public string Message { get; set; } = default!;
    public int StatusCode { get; set; }

    [JsonIgnore]
    public bool Successful { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Dictionary<object, object> Details { get; set; } = default!;

    public DateTime TimeStamp { get; } = DateTime.Now;
}

public static class ActionReporterProvider
{
    public static ActionReporter Set(string message, int statusCode, bool successful, Dictionary<object, object> details)
    {
        return new ActionReporter()
        {
            Message = message,
            StatusCode = statusCode,
            Successful = successful,
            Details = details
        };
    }

    public static ActionReporter Set(string message, int statusCode)
    {
        return new ActionReporter()
        {
            Message = message,
            StatusCode = statusCode
        };
    }

    public static ActionReporter Set(string message, int statusCode, Dictionary<object, object> details)
    {
        return new ActionReporter()
        {
            Message = message,
            StatusCode = statusCode,
            Details = details
        };
    }
}

