using eCommerceClean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eCommerceeCommerceClean.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using StoreDbContext dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
        dbContext.Database.Migrate();
    }
}
