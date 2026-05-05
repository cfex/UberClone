using MediatR;

namespace Location.Application.Locations.Commands.UpdateLocation;

public sealed record UpdateDriverLocationCommand(Guid DriverId, double Latitude, double Longitude) : IRequest;