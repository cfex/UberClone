namespace Ride.Application.Dto;

public record RideDetailsDto(Guid RideId, Guid? DriverId, Guid PassengerId, DateTime CreatedAt)
{
    public static RideDetailsDto Create(Guid rideId, Guid? driverId, Guid passengerId, DateTime createdAt)
    {
        return new RideDetailsDto(rideId, driverId, passengerId, createdAt);
    }
}