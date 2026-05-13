namespace Ride.Domain.ValueObjects;

public record Location(double Longitude, double Latitude)
{
    private const double EarthRadiusKm = 6371.0;
    private const double KmPerDegreeLat = 111.32;

    public static Location Create(double longitude, double latitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException($"Latitude must be between -90 and 90. Got: {latitude}");
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException($"Longitude must be between -180 and 180. Got: {longitude}");

        return new Location(longitude, latitude);
    }

    public double DistanceInKilometersTo(Location other)
    {
        var lat1Rad = DegreesToRadians(Latitude);
        var lat2Rad = DegreesToRadians(other.Latitude);
        var deltaLatRad = DegreesToRadians(other.Latitude - Latitude);
        var deltaLonRad = DegreesToRadians(other.Longitude - Longitude);

        var a = Math.Sin(deltaLatRad / 2) * Math.Sin(deltaLatRad / 2) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(deltaLonRad / 2) * Math.Sin(deltaLonRad / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusKm * c;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}