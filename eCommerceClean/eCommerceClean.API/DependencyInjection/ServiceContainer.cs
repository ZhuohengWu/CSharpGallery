using eCommerceClean.API.Exceptions;

namespace eCommerceClean.API.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        //services.AddExceptionHandler<GlobalExceptionHandler>();
        //services.AddProblemDetails();
        return services;
    }

    public static IApplicationBuilder UsePresentation(this WebApplication app)
    {
        app.UseStaticFiles();
        return app;
    }
}
