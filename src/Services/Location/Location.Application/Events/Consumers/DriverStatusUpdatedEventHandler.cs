using Location.Application.Abstraction;
using Microsoft.Extensions.Logging;
using Shared.Contracts.IntegrationEvents.Driver;

namespace Location.Application.Events.Consumers;

public class DriverStatusUpdatedEventHandler
{
    private readonly ILogger<DriverStatusUpdatedEventHandler> _logger;

    private readonly ILocationRepository _repository;

    public DriverStatusUpdatedEventHandler(ILogger<DriverStatusUpdatedEventHandler> logger,
        ILocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Handle(DriverStatusChangedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling driver status updated event");

        switch (@event.NewStatus)
        {
            case "offline":
                await _repository.RemoveDriverLocationAsync(@event.DriverId);
                break;
            // TODO: handle all other statuses (maybe)
            case "online":
            case "available":
                break;
            default:
                _logger.LogInformation($"New driver status {@event.NewStatus}");
                break;
        }
    }
}