using Ride.Domain.ValueObjects;

namespace Ride.API.Dtos;

public sealed record CreateRideRequestDto(string PassengerId, Location Destination);