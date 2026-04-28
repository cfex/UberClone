using Microsoft.Extensions.Logging;
using Shared.Contracts.IntegrationEvents.Driver;
using StackExchange.Redis;

namespace Location.Application.Events.Consumers;

public class DriverStatusUpdatedEventHandler
{
    private static readonly RedisKey DriverGeoKey = "driver:locations";

    private readonly IDatabase _cache;
    private readonly ILogger<DriverStatusUpdatedEventHandler> _logger;

    public DriverStatusUpdatedEventHandler(IDatabase cache, ILogger<DriverStatusUpdatedEventHandler> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task Handle(DriverStatusChangedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling driver status updated event");

        if (@event.NewStatus.Equals("offline", StringComparison.CurrentCultureIgnoreCase))
            await _cache.GeoRemoveAsync(DriverGeoKey, new RedisValue(@event.DriverId.ToString()));
    }
}