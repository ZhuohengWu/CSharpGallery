using eCommerceClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Application.Commons.Data;

public interface ICleanDbContext
{
    DbSet<ToDoTask> TodoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
