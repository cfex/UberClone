using Driver.Application.Dto;
using Driver.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Driver.Application.Drivers.Queries;

public class GetDriverDetailsQueryHandler : IRequestHandler<GetDriverDetailsQuery, DriverResponseDto>
{
    private readonly IDriverRepository _driverRepository;
    private readonly ILogger<GetDriverDetailsQueryHandler> logger;

    public GetDriverDetailsQueryHandler(IDriverRepository driverRepository,
        ILogger<GetDriverDetailsQueryHandler> logger)
    {
        _driverRepository = driverRepository;
        this.logger = logger;
    }

    public async Task<DriverResponseDto> Handle(GetDriverDetailsQuery request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(Guid.Parse(request.DriverId), cancellationToken);
        logger.LogInformation($"Driver {driver}  found");
        if (driver == null) throw new KeyNotFoundException("Driver not found");

        return DriverResponseDto.Create(driver.Id, driver.FullName.FirstName, driver.Email.Value,
            driver.Status.ToString(), driver.Fare.Amount);
    }
}