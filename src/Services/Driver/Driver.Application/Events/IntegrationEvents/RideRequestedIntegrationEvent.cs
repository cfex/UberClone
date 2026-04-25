using Driver.Domain.ValueObjects;

namespace Driver.Application.Events.IntegrationEvents;

public class RideRequestedIntegrationEvent
{
    public Guid RideId { get; set; }
    public List<Guid> DriverIds { get; set; }
    public Location Location { get; set; }
    public Location Destination { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}