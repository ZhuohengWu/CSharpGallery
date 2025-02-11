using eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrand.Any() )
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands != null && brands.Any())
                    {
                        context.ProductBrand.AddRange(brands); 
                        await context.SaveChangesAsync();
                    }
                }

                if (!context.ProductType.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types != null && types.Any())
                    {
                        context.ProductType.AddRange(types); 
                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products != null && products.Any())
                    {
                        foreach (var item in products)
                        {
                            var brand = await context.ProductBrand.FirstOrDefaultAsync(b => b.Id == item.ProductBrandId);
                            var type = await context.ProductType.FirstOrDefaultAsync(t => t.Id == item.ProductTypeId);
                            if (brand != null && type != null)
                            {
                                item.ProductBrand = brand;
                                item.ProductType = type;
                                context.Products.Add(item);
                            }
                        }

                        await context.SaveChangesAsync();
                    }                    
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
