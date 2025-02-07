using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Infrastructure.Persistence
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options), ICleanDbContext
    {
        public DbSet<ToDoTask> TodoItems => Set<ToDoTask>();

        public DbSet<Product> Products => Set<Product>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Scans the assembly to register all classes that implement IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
    }
}
