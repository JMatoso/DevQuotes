using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DevQuotes.Infrastructure.Options;
using DevQuotes.Infrastructure.Repository.Quotes;
using DevQuotes.Infrastructure.Repository.Languages;

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

                    if (corsOptions.AllowedOrigins is not [] || corsOptions.AllowedOrigins is not null)
                    {
                        builder.WithOrigins(corsOptions.AllowedOrigins)
                               .WithMethods("POST", "GET", "PUT", "DELETE"); 
                    }

                    builder.AllowAnyHeader();
                });
            });
        }

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddScoped<IQuotesRepository, QuotesRepository>();
        services.AddScoped<ILanguagesRepository, LanguagesRepository>();
    }
}
