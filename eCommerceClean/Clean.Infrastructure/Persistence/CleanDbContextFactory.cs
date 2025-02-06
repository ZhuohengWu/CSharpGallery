using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eCommerceClean.Infrastructure.Persistence;

public class CleanDbContextFactory() : IDesignTimeDbContextFactory<CleanDbContext>
{
    public CleanDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CleanDbContext>();

        var connectionString = "Server=(local); Database=CleanAppDb; Trusted_Connection=True; TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);

        return new CleanDbContext(optionsBuilder.Options);
    }
}
