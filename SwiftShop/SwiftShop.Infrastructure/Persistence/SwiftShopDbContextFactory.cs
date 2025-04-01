using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShop.Infrastructure.Persistence
{
    public class SwiftShopDbContextFactory() : IDesignTimeDbContextFactory<SwiftShopDbContext>
    {
        public SwiftShopDbContext CreateDbContext(string[] args)
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "../SwiftShop.Presentation");

            // Load configuration from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SwiftShopDbContext>();

            var connectionString = configuration.GetConnectionString("SwiftShopAppSqlServer");
            optionsBuilder.UseSqlServer(connectionString);

            return new SwiftShopDbContext(optionsBuilder.Options);
        }
    }
}
