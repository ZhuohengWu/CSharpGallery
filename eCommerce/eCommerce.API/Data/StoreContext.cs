using eCommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.API.Data
{
    public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
