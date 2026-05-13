using MediatR;
using Passenger.Application.Dto;

namespace Passenger.Application.Passengers.Queries;

public sealed record GetPassengerByIdCommand(Guid PassengerId) : IRequest<PassengerResponseDto>;