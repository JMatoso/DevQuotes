using Microsoft.AspNetCore.Builder;

namespace DevQuotes.Extensions.Services.Installers.Interfaces;

public interface IExtensionInstaller
{
    void InstallExtensions(WebApplication webApplication);
}
