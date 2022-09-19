using DevQuotes.Extensions.Logging;
using DevQuotes.Extensions.Services.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Development
});

builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.
builder.Services.InstallServices<Program>(builder.Configuration);

var app = builder.Build();

// Use middlewares.
app.InstallExtensions<Program>();
