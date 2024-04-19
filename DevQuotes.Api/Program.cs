using Serilog;
using DevQuotes.Infrastructure.Extensions;
using DevQuotes.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(LoggingExtensions.ConfigureLogger);
builder.Services.ConfigureApplication(builder.Configuration);

var app = builder.Build();
app.UseApplicationServices();