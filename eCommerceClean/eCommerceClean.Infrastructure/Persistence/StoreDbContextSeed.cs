using eCommerceClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eCommerceClean.Infrastructure.Persistence
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../eCommerceClean.Infrastructure/Persistence/SeedData/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands != null && brands.Any())
                {
                    await using var transaction = await dbContext.Database.BeginTransactionAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands ON;");

                    dbContext.ProductBrands.AddRange(brands);
                    await dbContext.SaveChangesAsync();

                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands OFF;");
                    await transaction.CommitAsync();
                }
            }

            if (!dbContext.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../eCommerceClean.Infrastructure/Persistence/SeedData/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types != null && types.Any())
                {
                    await using var transaction = await dbContext.Database.BeginTransactionAsync();
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductTypes ON;");

                    dbContext.ProductTypes.AddRange(types);
                    await dbContext.SaveChangesAsync();

                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductTypes OFF;");
                    await transaction.CommitAsync();
                }
            }

            if (!dbContext.Products.Any())
            {
                var productsData = File.ReadAllText("../eCommerceClean.Infrastructure/Persistence/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products != null && products.Any())
                {
                    foreach (var item in products)
                    {
                        var brand = await dbContext.ProductBrands.FirstOrDefaultAsync(b => b.Id == item.ProductBrandId);
                        var type = await dbContext.ProductTypes.FirstOrDefaultAsync(t => t.Id == item.ProductTypeId);
                        if (brand != null && type != null)
                        {
                            item.ProductBrand = brand;
                            item.ProductType = type;
                            dbContext.Products.Add(item);
                        }
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
