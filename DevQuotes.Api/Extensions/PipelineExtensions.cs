using DevQuotes.Infrastructure.Options;

namespace DevQuotes.Api.Extensions;

public static class PipelineExtensions
{
    public static void UseApplicationServices(this WebApplication app, IConfiguration configuration)
    {
        var corsOptions = configuration.GetSection(CorsOptions.Cors).Get<CorsOptions>();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthorization();
        app.UseAuthentication();

        if (corsOptions is not null)
        {
            app.UseCors(corsOptions.PolicyName);
        }

        app.MapControllers();

        app.Run();
    }
}
