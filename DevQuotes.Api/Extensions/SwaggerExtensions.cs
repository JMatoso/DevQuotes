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
                Title = "DevQuotes API",
                Version = $"v1",
                Description = @"The Coding Quotes API provides a curated collection of inspiring 
                                and motivational quotes tailored for programmers, developers, and 
                                technology enthusiasts.",
                Contact = new OpenApiContact
                {
                    Name = "José Matoso",
                    Email = "joaquimjose@duck.com",
                    Url = new Uri("https://github.com/JMatoso/")
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
