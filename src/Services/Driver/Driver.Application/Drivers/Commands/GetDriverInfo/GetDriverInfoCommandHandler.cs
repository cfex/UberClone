using Driver.Application.Dtos;
using Driver.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Driver.Application.Drivers.Commands.GetDriverInfo;

public class GetDriverInfoCommandHandler : IRequestHandler<GetDriverInfoCommand, DriverResponseDto>
{
    private readonly IDriverRepository _driverRepository;
    private readonly ILogger<GetDriverInfoCommandHandler> logger;

    public GetDriverInfoCommandHandler(IDriverRepository driverRepository, ILogger<GetDriverInfoCommandHandler> logger)
    {
        _driverRepository = driverRepository;
        this.logger = logger;
    }

    public async Task<DriverResponseDto> Handle(GetDriverInfoCommand request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(Guid.Parse(request.DriverId), cancellationToken);
        logger.LogInformation($"Driver {driver}  found");
        if (driver == null) throw new KeyNotFoundException("Driver not found");

        return DriverResponseDto.Create(driver.Id, driver.FullName.FirstName, driver.Email.Value, driver.Status.ToString(), driver.Fare);
    }
}