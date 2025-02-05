using Clean.Application.Commons.Data;
using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Persistence
{
    public class CleanDbContext(DbContextOptions options) : DbContext(options), ICleanDbContext
    {
        public DbSet<ToDoTask> TodoItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanDbContext).Assembly);
        }
    }
}
