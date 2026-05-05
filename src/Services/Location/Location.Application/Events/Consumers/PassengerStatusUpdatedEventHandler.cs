using Location.Application.Abstraction;
using Microsoft.Extensions.Logging;
using Shared.Contracts.IntegrationEvents.Passenger;

namespace Location.Application.Events.Consumers;

public class PassengerStatusUpdatedEventHandler
{
    private readonly ILogger<PassengerStatusUpdatedEventHandler> _logger;
    private readonly ILocationRepository _repository;

    public PassengerStatusUpdatedEventHandler(ILogger<PassengerStatusUpdatedEventHandler> logger,
        ILocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }


    public async Task Handle(PassengerStatsChangedIntegrationEvent @event)
    {
        _logger.LogInformation($"Handling {nameof(PassengerStatusUpdatedEventHandler)}");
        switch (@event.NewStatus)
        {
            case "offline":
                await _repository.RemoveDriverLocationAsync(@event.PassengerId);
                break;

            // TODO: handle all other statuses (maybe)
            default:
                _logger.LogInformation($"New passenger status {@event.NewStatus}");
                break;
        }
    }
}