using Driver.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Driver.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddDriverApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly, includeInternalTypes: true);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly); });

        return services;
    }
}