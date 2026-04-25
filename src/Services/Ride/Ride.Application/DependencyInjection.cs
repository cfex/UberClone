using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ride.Application.Behaviors;

namespace Ride.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddRideApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly, includeInternalTypes: true);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly); });

        return services;
    }
}