using DevQuotes.Extensions.Services.Installers.Interfaces;
using Microsoft.OpenApi.Models;

namespace DevQuotes.Server.Installers.Installers;

public class InstallSwagger : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc($"v1", new OpenApiInfo
            {
                Title = "DevQuotes",
                Version = $"v1",
                Description = "Simple quotes API.",
                Contact = new OpenApiContact
                {
                    Name = "José Matoso",
                    Email = "jos3matosoj@gmail.com",
                    Url = new Uri("https://github.com/JMatoso/DevQuotes")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/JMatoso/DevQuotes/blob/master/LICENSE.txt")
                },
                TermsOfService = new Uri("https://github.com/JMatoso/DevQuotes")
            });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            config.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });
    }
}
