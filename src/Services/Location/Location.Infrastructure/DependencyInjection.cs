using Location.Application;
using Location.Application.Abstraction;
using Location.Infrastructure.Configuration;
using Location.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Wolverine;
using Wolverine.RabbitMQ;

namespace Location.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddLocationInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqOptions = configuration.GetSection(RabbitMqOptions.SectionName).Get<RabbitMqOptions>()
                              ?? throw new InvalidOperationException("RabbitMQ configuration is missing or invalid.");
        var redisOptions = configuration.GetSection(RedisOptions.SectionName).Get<RedisOptions>()
                           ?? throw new InvalidOperationException("Redis configuration is missing or invalid.");

        services.AddGrpc();

        services.AddOptions<LocationGrpcOptions>()
            .Bind(configuration.GetSection(LocationGrpcOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddWolverine(x =>
        {
            x.Policies.DisableConventionalLocalRouting();
            x.UseRabbitMq(config =>
                {
                    config.HostName = rabbitMqOptions.Host;
                    config.VirtualHost = rabbitMqOptions.VHost;
                    config.UserName = rabbitMqOptions.Username;
                    config.Password = rabbitMqOptions.Password;
                    config.Port = rabbitMqOptions.Port;
                })
                .AutoProvision()
                .ConfigureSenders(opts => opts.UseDurableOutbox());

            x.ListenToRabbitQueue("driver-status-queue").UseForReplies().UseDurableInbox();

            x.Discovery.IncludeAssembly(typeof(AssemblyReference).Assembly);
        });


        services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { { redisOptions.Host, redisOptions.Port } },
                ConnectTimeout = 5000,
                SyncTimeout = 5000,
                AbortOnConnectFail = false,
                KeepAlive = 10
            };

            return ConnectionMultiplexer.Connect(config);
        });

        return services;
    }
}