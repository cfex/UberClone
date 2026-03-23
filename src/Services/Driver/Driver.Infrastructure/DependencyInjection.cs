using Driver.Application;
using Driver.Application.Abstractions;
using Driver.Domain.Events;
using Driver.Domain.Repositories;
using Driver.Infrastructure.Configuration;
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

        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();


        var rabbitMqOptions = configuration.GetSection(RabbitMqOptions.SectionName).Get<RabbitMqOptions>()
                              ?? throw new InvalidOperationException("RabbitMQ configuration is missing or invalid.");


        services.AddWolverine(x =>
        {
            x.Policies.DisableConventionalLocalRouting();
            x.UseRabbitMq(rabbit =>
            {
                rabbit.HostName = rabbitMqOptions.Host;
                rabbit.VirtualHost = rabbitMqOptions.VHost;
                rabbit.UserName = rabbitMqOptions.Username;
                rabbit.Password = rabbitMqOptions.Password;
            }).AutoProvision();

            x.ListenToRabbitQueue("user-created-events")
                .UseForReplies()
                .UseDurableInbox();
            x.ListenForMessagesFrom("ride-request-events")
                .UseForReplies()
                .UseDurableInbox();

            x.PublishMessage<DriverStatusChangedEvent>()
                .ToRabbitExchange("driver_events", ex =>
                {
                    ex.ExchangeType = ExchangeType.Topic;
                    ex.IsDurable = true;
                    ex.BindQueue("driver-events-queue");
                });
            x.PublishMessage<DriverCreatedEvent>()
                .ToRabbitExchange("driver-events", ex =>
                {
                    ex.ExchangeType = ExchangeType.Topic;
                    ex.IsDurable = true;
                    ex.BindQueue("driver-events-queue");
                });
            x.Discovery.IncludeAssembly(typeof(AssemblyReference).Assembly);
        });
        services.AddScoped<IEventBus, TransitEventBus>();

        return services;
    }
}