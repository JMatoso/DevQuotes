using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using DevQuotes.Extensions.Logging;
using DevQuotes.Extensions.Services.Installers;
using DevQuotes.Models.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Development
});

var sentryOptions = new SentryOptions();

builder.Configuration.Bind(nameof(SentryOptions), sentryOptions);
builder.Services.AddSingleton(sentryOptions);

builder.WebHost.UseSentry(options =>
{
    options.Debug = sentryOptions.Debug;
    options.TracesSampleRate = sentryOptions.TracesSampleRate;
    options.Dsn = sentryOptions.Dsn;

    options.MaxRequestBodySize = sentryOptions.MaxRequestBodySize;
    options.SendDefaultPii = sentryOptions.SendDefaultPii;
    options.MinimumBreadcrumbLevel = sentryOptions.MinimumBreadcrumbLevel;
    options.MinimumEventLevel = sentryOptions.MinimumEventLevel;
    options.DiagnosticLevel = sentryOptions.DiagnosticsLevel;
    options.AttachStacktrace = sentryOptions.AttachStackTrace;

    options.BeforeSend = @event =>
    {
        // Never report server names
        @event.ServerName = null;
        return @event;
    };
});

builder.Host
    .UseMetricsWebTracking()
    .UseMetrics(options =>
    {
        options.EndpointOptions = endpointsOptions =>
        {
            endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
            endpointsOptions.EnvironmentInfoEndpointEnabled = false;
        };
    })
    .UseSerilog(Logging.ConfigureLogger)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    });

// Add services to the container.
builder.Services.InstallServices<Program>(builder.Configuration);

var app = builder.Build();

// Use middlewares.
app.InstallExtensions<Program>();
