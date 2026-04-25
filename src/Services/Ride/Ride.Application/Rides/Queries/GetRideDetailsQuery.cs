using MediatR;
using Ride.Application.Dto;

namespace Ride.Application.Rides.Queries;

public sealed record GetRideDetailsQuery(Guid RideId) : IRequest<RideDetailsDto>
{
}