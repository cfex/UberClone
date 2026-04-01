using Ride.Domain.ValueObjects;

namespace Ride.API.Dtos;

public sealed record RideRequestDto(string PassengerId, Location Destination);