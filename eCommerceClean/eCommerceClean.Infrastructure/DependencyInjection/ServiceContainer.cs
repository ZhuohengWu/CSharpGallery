﻿using eCommerceClean.Application.Commons.Data;
using eCommerceClean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceClean.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) 
            => services.AddDatabase(config).AddHealthChecks(config);

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("ECommerceAppSqlServer");
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IStoreDbContext>(sp => sp.GetRequiredService<StoreDbContext>());
            return services;
        }

        private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("ECommerceAppSqlServer")!);
            return services;
        }
    }
}
