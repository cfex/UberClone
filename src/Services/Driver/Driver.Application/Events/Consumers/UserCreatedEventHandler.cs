using Driver.Application.Events.IntegrationEvents;
using Driver.Domain.Enums;
using Driver.Domain.Repositories;
using Driver.Domain.ValueObjects;

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

    public async Task Handle(DriverCreatedIntegrationEvent message)
    {
        if (!message.Role.ToLower().Equals("driver")) return;

        var existingDriver = await _driverRepository.GetByEmail(message.Email);
        if (existingDriver != null) return;

        var driver = Domain.Entities.Driver.Create(
            message.UserId,
            FullName.Create(message.FirstName, message.LastName),
            Email.Create(message.Email),
            DriverStatus.New,
            Money.Create(0, Currency.USD),
            message.Role, null, null
        );

        await _driverRepository.CreateAsync(driver);
        await _unitOfWork.SaveChangesAsync();
    }
}