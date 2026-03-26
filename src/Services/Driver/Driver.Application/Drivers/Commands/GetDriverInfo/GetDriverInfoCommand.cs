using Driver.Application.Dtos;
using MediatR;

namespace Driver.Application.Drivers.Commands.GetDriverInfo;

public sealed record GetDriverInfoCommand(string DriverId) : IRequest<DriverResponseDto>;