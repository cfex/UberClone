namespace Shared.Contracts.IntegrationEvents.Driver;

public record DriverCreatedIntegrationEvent : IntegrationEvent
{
    public required Guid DriverId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Role { get; init; }
}