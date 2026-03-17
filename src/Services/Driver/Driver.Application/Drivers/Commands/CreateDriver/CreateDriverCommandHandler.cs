using Driver.Domain.Enums;
using Driver.Domain.Repositories;
using Driver.Domain.ValueObjects;
using MediatR;

namespace Driver.Application.Drivers.Commands.CreateDriver;

internal sealed class CreateDriverCommandHandler
    : IRequestHandler<CreateDriverCommand, Guid>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDriverCommandHandler(IDriverRepository driverRepository, IUnitOfWork unitOfWork)
    {
        _driverRepository = driverRepository;
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
            null,
            null);

        await _driverRepository.CreateAsync(driver, cancellationToken);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

        return driver.Id;
    }
}