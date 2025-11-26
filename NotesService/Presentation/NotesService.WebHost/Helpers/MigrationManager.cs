using Microsoft.EntityFrameworkCore;

namespace NotesService.WebHost.Helpers;
public static class MigrationManager
{
    public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
    {
        var scope = host.Services.CreateScope();
        var appContext = scope.ServiceProvider.GetService<T>();
        appContext?.Database.Migrate();
        return host;
    }
}