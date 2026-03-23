namespace Driver.Application.Dtos;

public sealed record DriverResponseDto(
    Guid Id,
    string FirstName,
    string Email
)
{
    public static DriverResponseDto Create(Guid driverId, string firstName, string email)
    {
        return new DriverResponseDto(driverId, firstName, email);
    }
}