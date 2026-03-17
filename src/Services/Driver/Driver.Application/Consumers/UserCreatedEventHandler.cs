using Driver.Application.IntegrationEvents;
using Driver.Domain.Enums;
using Driver.Domain.Repositories;
using Driver.Domain.ValueObjects;

namespace Driver.Application.Consumers;

public class UserCreatedEventHandler
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCreatedEventHandler(IUnitOfWork unitOfWork, IDriverRepository driverRepository)
    {
        _unitOfWork = unitOfWork;
        _driverRepository = driverRepository;
    }

    public async Task Handle(UserCreatedIntegrationEvent message)
    {
        await _unitOfWork.BeginTransactionAsync();
        if (!message.Role.ToLower().Equals("driver")) return; // throw probably

        var existingDriver = await _driverRepository.GetByEmail(message.Email);
        if (existingDriver != null) return;

        var driver = Domain.Entities.Driver.Create(
            Guid.Parse(message.UserId),
            FullName.Create(message.FirstName, message.LastName),
            Email.Create(message.Email),
            DriverStatus.New,
            Money.Create(0, Currency.USD),
            null,
            null
        );

        await _driverRepository.CreateAsync(driver);
        await _unitOfWork.CommitTransactionAsync();
    }
}