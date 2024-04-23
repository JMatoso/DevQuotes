using Asp.Versioning;
using DevQuotes.Application.Extensions;
using DevQuotes.Infrastructure.Extensions;

namespace DevQuotes.Api.Extensions;

public static class ApplicationExtensions
{
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(opt =>
        {
            opt.RespectBrowserAcceptHeader = true;
            opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });

        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

        services.AddSwaggerService();
        services.AddApplicationServices();
        services.AddInfrastrutures(configuration);
        services.AddDatabaseContext(configuration);
    }
}
