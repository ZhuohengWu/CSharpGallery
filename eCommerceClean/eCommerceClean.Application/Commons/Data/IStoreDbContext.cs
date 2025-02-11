using eCommerceClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Application.Commons.Data;

public interface IStoreDbContext
{
    //DbSet<ToDoTask> TodoItems { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductBrand> ProductBrand { get; }
    DbSet<ProductType> ProductType { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
