using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.API.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task ApplyMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                try
                {
                    var dbContext = service.GetRequiredService<StoreContext>();
                    await dbContext.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occured during migration");
                }

            }
        }
    }
}
