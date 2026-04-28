using Driver.Domain.Enums;
using Driver.Domain.Repositories;
using Driver.Domain.ValueObjects;
using Shared.Contracts.IntegrationEvents.Driver;

namespace Driver.Application.Events.Consumers;

public class UserCreatedEventHandler
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCreatedEventHandler(IUnitOfWork unitOfWork, IDriverRepository driverRepository)
    {
        _unitOfWork = unitOfWork;
        _driverRepository = driverRepository;
    }

    public async Task Handle(DriverCreatedIntegrationEvent @event)
    {
        if (!@event.Role.ToLower().Equals("driver")) return;

        var existingDriver = await _driverRepository.GetByEmailAsync(@event.Email);
        if (existingDriver != null) return;

        var driver = Domain.Entities.Driver.Create(
            @event.DriverId,
            FullName.Create(@event.FirstName, @event.LastName),
            Email.Create(@event.Email),
            DriverStatus.New,
            Money.Create(0, Currency.USD),
            @event.Role, null, null
        );

        await _driverRepository.CreateAsync(driver);
        await _unitOfWork.SaveChangesAsync();
    }
}