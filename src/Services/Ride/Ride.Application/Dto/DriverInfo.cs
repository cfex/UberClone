namespace Ride.Application.Dto;

public record DriverInfoDto(
    string DriverId,
    string Name,
    string Status,
    double Fare
)
{
    public static DriverInfoDto Create(string driverId, string name, string status, double fare)
    {
        return new DriverInfoDto(driverId, name, status, fare);
    }
}