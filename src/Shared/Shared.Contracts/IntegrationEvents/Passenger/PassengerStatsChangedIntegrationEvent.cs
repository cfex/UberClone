namespace Shared.Contracts.IntegrationEvents.Passenger;

public class PassengerStatsChangedIntegrationEvent
{
    public required Guid PassengerId { get; init; }
    public required string OldStatus { get; init; }
    public required string NewStatus { get; init; }
}