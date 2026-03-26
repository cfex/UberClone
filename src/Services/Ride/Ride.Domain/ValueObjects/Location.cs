namespace Ride.Domain.ValueObjects;

public record Location(double Longitude, double Latitude)
{
    public static Location Create(double longitude, double latitude)
    {
        return new Location(longitude, latitude);
    }
}