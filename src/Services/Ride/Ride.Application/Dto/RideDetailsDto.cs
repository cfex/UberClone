namespace Ride.Application.Dto;

public record RideDetailsDto(Guid RideId, Guid? DriverId, Guid PassengerId, DateTime CreatedAt, DateTime UpdatedAt)
{
    public static RideDetailsDto Create(Guid rideId, Guid? driverId, Guid passengerId, DateTime createdAt,
        DateTime updatedAt)
    {
        return new RideDetailsDto(rideId, driverId, passengerId, createdAt, updatedAt);
    }
}