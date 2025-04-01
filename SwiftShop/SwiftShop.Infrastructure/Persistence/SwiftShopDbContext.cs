using Microsoft.EntityFrameworkCore;
using SwiftShop.Application.Commons.Data;
using SwiftShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShop.Infrastructure.Persistence
{
    public class SwiftShopDbContext(DbContextOptions<SwiftShopDbContext> options) : DbContext(options), ISwiftShopDbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Scans the assembly to register all classes that implement IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SwiftShopDbContext).Assembly);
        }
    }
}
