using Driver.Domain.Repositories;
using Driver.Infrastructure.Persistence;
using Driver.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Driver.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDriverInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DriverDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DriverDbConnectionString"));
        });

        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}