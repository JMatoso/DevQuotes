using DevQuotes.Infrastructure.Options;

namespace DevQuotes.Api.Extensions;

public static class PipelineExtensions
{
    public static void UseApplicationServices(this WebApplication app, IConfiguration configuration)
    {
        var corsOptions = configuration.GetSection(CorsOptions.Cors).Get<CorsOptions>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        if (corsOptions is not null)
        {
            app.UseCors(corsOptions.PolicyName);
        }

        app.MapControllers();

        app.Run();
    }
}
