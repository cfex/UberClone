namespace Shared.Contracts.IntegrationEvents.Driver;

public record DriverStatusChangedIntegrationEvent : IntegrationEvent
{
    public required Guid DriverId { get; init; }
    public required string OldStatus { get; init; }
    public required string NewStatus { get; init; }
}