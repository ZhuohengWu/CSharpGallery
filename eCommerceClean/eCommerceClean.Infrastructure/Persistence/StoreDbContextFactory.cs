using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eCommerceClean.Infrastructure.Persistence;

public class StoreDbContextFactory() : IDesignTimeDbContextFactory<StoreDbContext>
{
    public StoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();

        var connectionString = "Server=(local); Database=ECommerceDb; Trusted_Connection=True; TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);

        return new StoreDbContext(optionsBuilder.Options);
    }
}
