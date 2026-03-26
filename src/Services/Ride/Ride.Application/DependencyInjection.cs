using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ride.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddRideApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly); });

        return services;
    }
}