namespace Ride.Application.Dto;

public record NearestDriverResponseDto(Guid DriverId, double Distance)
{
    public static NearestDriverResponseDto Create(Guid driverId, double distance)
    {
        return new NearestDriverResponseDto(driverId, distance);
    }
}