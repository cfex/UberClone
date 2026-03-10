using Driver.Application.Enums;
using MediatR;

namespace Driver.Application.Drivers.Commands.UpdateDriverStatus;

public sealed record UpdateDriverStatusCommand(
    Guid DriverId,
    DriverStatusAction Status
) : IRequest<Unit>;