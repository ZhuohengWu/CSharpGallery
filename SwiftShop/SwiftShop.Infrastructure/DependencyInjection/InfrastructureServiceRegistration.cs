using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwiftShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShop.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
            => services.AddDatabase(config);

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("SwiftShopAppSqlServer");
            services.AddDbContext<SwiftShopDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<SwiftShopDbContext>(sp => sp.GetRequiredService<SwiftShopDbContext>());
            return services;
        }
    }
}
