using Ride.Domain.ValueObjects;

namespace Ride.API.Dto;

public sealed record CreateRideRequestDto(string PassengerId, Location Destination);