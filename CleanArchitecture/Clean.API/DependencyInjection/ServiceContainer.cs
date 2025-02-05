namespace Clean.API.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static IApplicationBuilder UsePresentation(this WebApplication app)
    {
        return app;
    }
}
