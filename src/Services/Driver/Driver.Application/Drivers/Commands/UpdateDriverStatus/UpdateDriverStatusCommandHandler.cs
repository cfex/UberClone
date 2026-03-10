using Driver.Application.Enums;
using Driver.Domain.Repositories;
using MediatR;

namespace Driver.Application.Drivers.Commands.UpdateDriverStatus;

public record UpdateDriverStatusCommandHandler : IRequestHandler<UpdateDriverStatusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDriverStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateDriverStatusCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(request.DriverId, cancellationToken);
        if (driver == null) throw new Exception("Driver not found"); // TODO: Custom exceptions;

        switch (request.Status)
        {
            case DriverStatusAction.GoActive:
            case DriverStatusAction.GoAvailable:
            case DriverStatusAction.GoBusy:
            case DriverStatusAction.StartRide:
            case DriverStatusAction.StartCommuting:
                break;
            case DriverStatusAction.GoOffline:
                driver.GoOffline();
                break;
            case DriverStatusAction.GoOnline:
                driver.GoOnline();
                break;
            default:
                driver.GoOffline();
                break;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}