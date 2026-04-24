namespace Driver.Application.Events.IntegrationEvents;

public class DriverStatusChangedIntegrationEvent
{
    public Guid DriverId { get; set; }
    public string OldStatus { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
}