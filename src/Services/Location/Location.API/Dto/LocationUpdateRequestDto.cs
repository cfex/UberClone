namespace Location.API.Dto;

public record LocationUpdateRequestDto(double Latitude, double Longitude)
{
    public static LocationUpdateRequestDto Create(double latitude, double longitude)
    {
        return new LocationUpdateRequestDto(latitude, longitude);
    }
}