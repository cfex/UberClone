namespace Ride.Application.Dto;

public record CreateRideRequestDto(string PassengerId, double PickupLocation, double Destination)
{
}