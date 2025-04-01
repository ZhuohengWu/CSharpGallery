using Microsoft.Extensions.Options;

namespace SwiftShop.Presentation.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddCorsPolicy();

            return services;
        }
        private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") 
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
    }
}
