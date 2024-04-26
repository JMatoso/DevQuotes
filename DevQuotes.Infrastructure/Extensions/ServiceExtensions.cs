using DevQuotes.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using DevQuotes.Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using DevQuotes.Infrastructure.Options;

namespace DevQuotes.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastrutures(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = configuration.GetSection(CorsOptions.Cors).Get<CorsOptions>();

        if (corsOptions is not null)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(corsOptions.PolicyName, builder =>
                {
                    builder.AllowAnyOrigin().WithMethods("GET");
                    builder.WithOrigins(corsOptions.AllowedOrigins).WithMethods("POST", "GET", "PUT", "DELETE");
                    builder.AllowAnyHeader();
                });
            });
        }

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddScoped<IQuotesRepository, QuotesRepository>();
    }
}
