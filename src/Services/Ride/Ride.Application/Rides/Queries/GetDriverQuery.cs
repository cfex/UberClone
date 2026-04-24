using MediatR;
using Ride.Application.Dto;

namespace Ride.Application.Rides.Queries;

public sealed record GetDriverQuery(Guid DriverId) : IRequest<DriverInfoDto>
{
}