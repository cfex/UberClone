using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ride.Application.Abstractions;
using Ride.Application.Events.IntegrationEvents;
using Ride.Domain.Repositories;
using Ride.Infrastructure.Configuration;
using Ride.Infrastructure.Grpc;
using Ride.Infrastructure.MessageQueue;
using Ride.Infrastructure.Persistence;
using Ride.Infrastructure.Repositories;
using Ride.Infrastructure.Services.gRPC;
using Wolverine;
using Wolverine.RabbitMQ;

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

        var rabbitMqOptions = configuration.GetSection(RabbitMqOptions.SectionName).Get<RabbitMqOptions>()
                              ?? throw new InvalidOperationException("RabbitMQ configuration is missing or invalid.");


        services.AddScoped<IRideRepository, RideRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddWolverine(x =>
        {
            x.Policies.DisableConventionalLocalRouting();
            x.UseRabbitMq(rabbit =>
                {
                    rabbit.HostName = rabbitMqOptions.Host;
                    rabbit.VirtualHost = rabbitMqOptions.VHost;
                    rabbit.UserName = rabbitMqOptions.Username;
                    rabbit.Password = rabbitMqOptions.Password;
                }).AutoProvision()
                .ConfigureSenders(opts => opts.UseDurableOutbox());

            x.ListenToRabbitQueue("driver-status-queue")
                .UseForReplies()
                .UseDurableInbox();

            x.PublishMessage<RideRequestedDispatchEvent>()
                .ToRabbitExchange("ride-requests", ex =>
                {
                    ex.ExchangeType = ExchangeType.Topic;
                    ex.IsDurable = true;
                    ex.BindQueue("ride-requests-queue");
                });
            x.Discovery.IncludeAssembly(typeof(AssemblyReference).Assembly);
        });

        var driverGrpcAddress = configuration["GrpcServices:DriverService"] ?? "http://localhost:5001";

        services.AddGrpcClient<DriverService.DriverServiceClient>(options =>
        {
            options.Address = new Uri(driverGrpcAddress);
        });

        services.AddScoped<IDriverGrpcClient, DriverGrpcService>();
        services.AddScoped<IEventBus, TransitEventBus>();

        return services;
    }
}