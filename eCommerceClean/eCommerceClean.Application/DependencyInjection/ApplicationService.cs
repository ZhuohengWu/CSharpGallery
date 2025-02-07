using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using FluentValidation;
using eCommerceClean.Application.Commons.Behaviors;

namespace eCommerceClean.Application.DependencyInjection;

public static class ApplicationService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationService).Assembly, includeInternalTypes: true);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ApplicationService).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        return services;
    }
}
