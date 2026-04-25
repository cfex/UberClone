namespace Ride.API.Dto;

public record RideDetailsDto(Guid RideId, Guid DriverId, Guid PassengerId, DateTime CreatedAt, DateTime UpdatedAt);