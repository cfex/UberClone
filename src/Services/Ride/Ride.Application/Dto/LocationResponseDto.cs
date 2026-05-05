namespace Ride.Application.Dto;

public record LocationResponseDto(double Latitude, double Longitude)
{
    public static LocationResponseDto Create(double latitude, double longitude)
    {
        return new LocationResponseDto(latitude, longitude);
    }
}