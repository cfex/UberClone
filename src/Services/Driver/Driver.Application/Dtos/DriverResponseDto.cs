using Driver.Domain.ValueObjects;

namespace Driver.Application.Dtos;

public sealed record DriverResponseDto(
    Guid Id,
    string FirstName,
    string Email,
    string Status,
    Money Fare
)
{
    public static DriverResponseDto Create(Guid driverId, string firstName, string email, string status, Money fare)
    {
        return new DriverResponseDto(driverId, firstName, email, status, fare);
    }
}