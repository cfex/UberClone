using MediatR;

namespace Ride.Application.Rides.Commands.AcceptRide;

public record AcceptRideCommand(Guid DriverId, Guid RideId) : IRequest<Guid>; // todo: return more info