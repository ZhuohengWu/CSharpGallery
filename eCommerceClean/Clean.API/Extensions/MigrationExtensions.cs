using eCommerceClean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eCommerceeCommerceClean.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using CleanDbContext dbContext = scope.ServiceProvider.GetRequiredService<CleanDbContext>();
        dbContext.Database.Migrate();
    }
}
