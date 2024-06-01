using Serilog;
using DevQuotes.Infrastructure.Extensions;
using DevQuotes.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Render Configuration
if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
{
    builder.WebHost.ConfigureKestrel((context, serverOptions) =>
    {
        var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
        serverOptions.ListenAnyIP(int.Parse(port));
    });
}

builder.Host.UseSerilog(LoggingExtensions.ConfigureLogger);
builder.Services.ConfigureApplication(builder.Configuration);

var app = builder.Build();
app.UseApplicationServices(builder.Configuration);