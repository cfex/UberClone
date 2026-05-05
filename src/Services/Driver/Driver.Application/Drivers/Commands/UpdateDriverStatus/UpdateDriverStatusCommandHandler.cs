using Driver.Application.Enums;
using Driver.Domain.Repositories;
using MediatR;

namespace Driver.Application.Drivers.Commands.UpdateDriverStatus;

internal sealed class UpdateDriverStatusCommandHandler : IRequestHandler<UpdateDriverStatusCommand, Unit>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDriverStatusCommandHandler(IUnitOfWork unitOfWork, IDriverRepository driverRepository)
    {
        _unitOfWork = unitOfWork;
        _driverRepository = driverRepository;
    }

    public async Task<Unit> Handle(UpdateDriverStatusCommand request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(request.DriverId, cancellationToken);
        if (driver == null) throw new Exception("Driver not found");

        var isStatusValid = Enum.TryParse(request.Status, out DriverStatusAction status);
        if (!isStatusValid)
            throw new Exception("Status is not valid");

        switch (status)
        {
            case DriverStatusAction.GoActive:
            case DriverStatusAction.GoAvailable:
                driver.GoAvailable();
                break;
            case DriverStatusAction.GoBusy:
                driver.GoOffline();
                break;
            case DriverStatusAction.StartRide:
                driver.StartRide();
                break;
            case DriverStatusAction.StartCommuting:
                driver.StartCommuting();
                break;
            case DriverStatusAction.GoOffline:
                driver.GoOffline();
                break;
            case DriverStatusAction.GoOnline:
                driver.GoOnline();
                break;
            default:
                throw new Exception("Unknown status");
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}