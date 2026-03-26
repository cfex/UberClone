using MediatR;
using Ride.Domain.ValueObjects;

namespace Ride.Application.Rides.Commands.RequestRide;

public sealed record RequestRideCommand(Guid PassengerId, Location Destination) : IRequest<Guid>;