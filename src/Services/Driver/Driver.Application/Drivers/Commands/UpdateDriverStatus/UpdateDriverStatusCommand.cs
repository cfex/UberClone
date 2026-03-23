using MediatR;

namespace Driver.Application.Drivers.Commands.UpdateDriverStatus;

public sealed record UpdateDriverStatusCommand(
    Guid DriverId,
    string Status
) : IRequest<Unit>;