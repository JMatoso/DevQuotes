using Microsoft.Extensions.Logging;

namespace DevQuotes.Models.Options;

public class SentryOptions
{
    public string Dsn { get; set; } = default!;
    public double TracesSampleRate { get; set; }
    public Sentry.Extensibility.RequestSize MaxRequestBodySize { get; set; }
    public bool SendDefaultPii { get; set; }
    public LogLevel MinimumBreadcrumbLevel { get; set; }
    public LogLevel MinimumEventLevel { get; set; }
    public bool AttachStackTrace { get; set; }
    public bool Debug { get; set; }
    public Sentry.SentryLevel DiagnosticsLevel { get; set; }
}