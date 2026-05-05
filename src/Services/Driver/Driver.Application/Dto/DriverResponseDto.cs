namespace Driver.Application.Dto;

public sealed record DriverResponseDto(
    Guid Id,
    string FirstName,
    string Email,
    string Status,
    double Fare
)
{
    public static DriverResponseDto Create(Guid driverId, string firstName, string email, string status, double fare)
    {
        return new DriverResponseDto(driverId, firstName, email, status, fare);
    }
}