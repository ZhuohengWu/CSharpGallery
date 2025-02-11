using eCommerceClean.Domain.Entities;
using eCommerceClean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eCommerceeCommerceClean.API.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occured during migration");
        }
    }

    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            await StoreDbContextSeed.SeedAsync(dbContext);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during data seeding");
        }
    }
}
