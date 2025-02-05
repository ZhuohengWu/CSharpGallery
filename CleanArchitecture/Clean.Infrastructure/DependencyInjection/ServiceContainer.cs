using Clean.Application.Commons.Data;
using Clean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) 
            => services.AddDatabase(config).AddHealthChecks(config);

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("CleanAppSqlServer");
            services.AddDbContext<CleanDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ICleanDbContext>(sp => sp.GetRequiredService<CleanDbContext>());
            return services;
        }

        private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("CleanAppSqlServer")!);
            return services;
        }
    }
}
