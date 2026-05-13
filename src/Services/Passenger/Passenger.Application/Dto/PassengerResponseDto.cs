using Passenger.Domain.Enums;
using Passenger.Domain.ValueObjects;

namespace Passenger.Application.Dto;

public sealed record PassengerResponseDto(FullName FullName, Email Email, PassengerStatus Status)
{
    public static PassengerResponseDto Create(FullName fullName, Email email, PassengerStatus status)
    {
        return new PassengerResponseDto(fullName, email, status);
    }
}