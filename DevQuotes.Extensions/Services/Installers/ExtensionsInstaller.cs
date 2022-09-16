using DevQuotes.Extensions.Services.Installers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuotes.Extensions.Services.Installers;

public static class ExtensionsInstaller
{
    public static void InstallServices<T>(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceInstallers = typeof(T).Assembly.ExportedTypes
            .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>()
            .ToList();

        serviceInstallers.ForEach(installer => installer.InstallServices(services, configuration));
    }

    public static void InstallExtensions<T>(this WebApplication webApplication)
    {
        var extensionInstaller = typeof(T).Assembly.ExportedTypes
            .Where(x => typeof(IExtensionInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IExtensionInstaller>()
            .ToList();

        extensionInstaller.ForEach(installer => installer.InstallExtensions(webApplication));
    }
}

