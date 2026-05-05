using Driver.Application.Dto;
using MediatR;

namespace Driver.Application.Drivers.Queries;

public sealed record GetAllDriversQuery : IRequest<List<DriverResponseDto>>;