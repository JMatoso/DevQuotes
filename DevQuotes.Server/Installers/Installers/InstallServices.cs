using DevQuotes.Extensions.Filters;
using DevQuotes.Extensions.Services.Installers.Interfaces;
using DevQuotes.Server.Data;
using DevQuotes.Server.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DevQuotes.Server.Installers.Installers;

public class InstallServices : IServiceInstaller
{
    void IServiceInstaller.InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

        services
            .AddCors()
            .AddHttpContextAccessor()
            .AddScoped<ValidationFilterAttribute>()
            .AddScoped(typeof(IMockRepository<>), typeof(MockRepository<>))
            .AddSqlite<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection"), options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

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

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
