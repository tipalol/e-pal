using Epal.Api.Hubs;
using Epal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Epal.Api.Configuration;

public static class AppConfiguration
{
    public static WebApplication ConfigureAppPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.MapHub<EpalHub>("/status");

        app.UseCors("AllowAll");
        app.MapControllers();

        app.UseAuthentication();
        app.UseAuthorization();

        MigrateDbContext(app);

        return app;
    }

    private static void MigrateDbContext(IHost app)
    {
        using var scope = app.Services.CreateScope();
        var databaseFacade = scope.ServiceProvider.GetRequiredService<EpalDbContext>().Database;

        var migrations = databaseFacade.GetPendingMigrations().ToArray();
        if (migrations.Length > 0)
        {
            Console.WriteLine(
                $"Migrate database: {Environment.NewLine}{string.Join(Environment.NewLine, migrations)}");
        }

        databaseFacade.Migrate();
    }
}
