using Driver.Application.Abstractions;
using Driver.Domain.Repositories;
using Driver.Infrastructure.MessageQueue;
using Driver.Infrastructure.Persistence;
using Driver.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;
using Wolverine.RabbitMQ;

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
        services.AddWolverine(x =>
        {
            x.UseRabbitMq(rabbit =>
            {
                rabbit.HostName = configuration["RabbitMQ:Host"] ?? "";
                rabbit.VirtualHost = configuration["RabbitMQ:VHost"] ?? "";
                rabbit.UserName = configuration["RabbitMQ:Username"] ?? "";
                rabbit.Password = configuration["RabbitMQ:Password"] ?? "";
            });
        });
        services.AddScoped<IEventBus, TransitEventBus>();

        return services;
    }
}