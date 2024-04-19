using DevQuotes.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using DevQuotes.Infrastructure.Filters;

namespace DevQuotes.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastrutures(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("DevQuotesCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddScoped<IQuotesRepository, QuotesRepository>();
    }
}
