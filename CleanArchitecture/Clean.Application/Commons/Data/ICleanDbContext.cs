using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Commons.Data;

public interface ICleanDbContext
{
    DbSet<ToDoTask> TodoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
