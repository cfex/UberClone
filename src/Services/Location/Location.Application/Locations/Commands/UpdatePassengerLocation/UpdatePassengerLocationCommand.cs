using MediatR;

namespace Location.Application.Locations.Commands.UpdatePassengerLocation;

public sealed record UpdatePassengerLocationCommand(Guid PassengerId, double Latitude, double Longitude) : IRequest;