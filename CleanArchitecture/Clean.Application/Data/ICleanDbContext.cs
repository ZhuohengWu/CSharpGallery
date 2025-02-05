using Clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Data;

public interface ICleanDbContext
{
    DbSet<ToDoTask> TodoItems {  get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
