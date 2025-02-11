using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Infrastructure.Persistence
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options), IStoreDbContext
    {
        //public DbSet<ToDoTask> TodoItems => Set<ToDoTask>();

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductBrand> ProductBrand => Set<ProductBrand>();
        public DbSet<ProductType> ProductType => Set<ProductType>();
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
