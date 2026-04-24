using Ride.Application.Abstractions;
using Wolverine;

namespace Ride.Infrastructure.MessageQueue;

public class TransitEventBus : IEventBus
{
    private readonly IMessageBus _messageBus;

    public TransitEventBus(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : class
    {
        await _messageBus.PublishAsync(integrationEvent);
    }
}