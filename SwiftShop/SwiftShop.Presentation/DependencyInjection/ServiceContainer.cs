using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SwiftShop.Infrastructure.Persistence;

namespace SwiftShop.Presentation.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCorsPolicy(configuration);

            return services;
        }
        private static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetValue<string>("AllowedOrigins").Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigins) 
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            return services;
        }

        public static IApplicationBuilder UsePresentation(this IApplicationBuilder app)
        {
            app.UseCors("AllowAngularApp");

            return app;
        }

        private static IApplicationBuilder ApplyDbMigration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SwiftShopDbContext>();

                var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
                if (env.IsProduction())
                {
                    dbContext.Database.Migrate();
                }
            }

            return app;
        }
    }
}
