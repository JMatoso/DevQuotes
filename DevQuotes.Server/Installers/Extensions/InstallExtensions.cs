using DevQuotes.Extensions.Services.Installers.Interfaces;

namespace DevQuotes.Server.Installers.Extensions;

public class InstallExtensions : IExtensionInstaller
{
    void IExtensionInstaller.InstallExtensions(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/error-local-development");
        }

        // On purpose
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors(opt => {
            opt.AllowAnyHeader();
            opt.AllowAnyHeader();
            opt.SetIsOriginAllowed((host) => true);
            opt.AllowCredentials();
        });

        app.MapControllers();

        app.Run();
    }
}
