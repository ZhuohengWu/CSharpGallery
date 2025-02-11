using eCommerceClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceClean.Application.Commons.Data;

public interface IStoreDbContext
{
    //DbSet<ToDoTask> TodoItems { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductBrand> ProductBrands { get; }
    DbSet<ProductType> ProductTypes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
