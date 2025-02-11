using eCommerceClean.Domain.Entities;
using eCommerceClean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eCommerceeCommerceClean.API.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        try
        {
            using StoreDbContext dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occured during migration");
        }
    }

    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

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
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during data seeding");
        }
    }


}
