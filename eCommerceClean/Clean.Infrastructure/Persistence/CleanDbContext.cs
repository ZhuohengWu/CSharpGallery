using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Infrastructure.Persistence
{
    public class CleanDbContext(DbContextOptions<CleanDbContext> options) : DbContext(options), ICleanDbContext
    {
        public DbSet<ToDoTask> TodoItems => Set<ToDoTask>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Scans the assembly to register all classes that implement IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanDbContext).Assembly);
        }
    }
}
