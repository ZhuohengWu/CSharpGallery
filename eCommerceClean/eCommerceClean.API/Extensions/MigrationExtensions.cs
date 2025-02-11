using eCommerceClean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eCommerceeCommerceClean.API.Extensions;

public static class MigrationExtensions
{
    public static async void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        try
        {
            using StoreDbContext dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occured during migration");
        }
    }
}
