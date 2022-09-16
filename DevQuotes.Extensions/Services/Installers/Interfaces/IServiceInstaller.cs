using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuotes.Extensions.Services.Installers.Interfaces;

public interface IServiceInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}
