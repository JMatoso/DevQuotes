using Microsoft.OpenApi.Models;

namespace DevQuotes.Api.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc($"v1", new OpenApiInfo
            {
                Title = "DevQuotes",
                Version = $"v1",
                Description = "Coding quotes API.",
                Contact = new OpenApiContact
                {
                    Name = "DevQuotes",
                    Email = "joaquimjose@duck.com",
                    Url = new Uri("https://codequotes-nine.vercel.app/")
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
