using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevQuotes.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), cfg =>
                cfg.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

#if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
#endif
        });
    }
}
