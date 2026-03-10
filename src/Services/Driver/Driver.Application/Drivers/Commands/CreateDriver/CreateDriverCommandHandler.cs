using Driver.Domain.Entities;
using Driver.Domain.Enums;
using Driver.Domain.Repositories;
using Driver.Domain.ValueObjects;
using MediatR;

namespace Driver.Application.Drivers.Commands.CreateDriver;

public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDriverCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = Domain.Entities.Driver.Create(
            Guid.NewGuid(),
            FullName.Create(request.FirstName, request.LastName),
            Email.Create(request.Email),
            DriverStatus.New,
            Money.Create(request.FareAmount, request.Currency),
            Document.Create(request.DocumentType, request.DocumentExpiryDate),
            Vehicle.Create(request.VehicleMake, request.VehicleModel,
                request.VehicleLicensePlate, request.VehicleColor,
                request.VehicleRegistrationDate));

        await _unitOfWork.Drivers.CreateAsync(driver, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return driver.Id;
    }
}