using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ride.Domain.Repositories;
using Ride.Infrastructure.Persistence;
using Ride.Infrastructure.Repositories;

namespace Ride.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRideInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RideDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("RideDbConnectionString"));
        });

        services.AddScoped<IRideRepository, RideRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}