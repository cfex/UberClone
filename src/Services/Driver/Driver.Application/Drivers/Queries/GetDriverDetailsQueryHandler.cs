using Driver.Application.Dto;
using Driver.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Driver.Application.Drivers.Queries;

public class GetDriverDetailsQueryHandler : IRequestHandler<GetDriverDetailsQuery, DriverResponseDto>
{
    private readonly IDriverRepository _driverRepository;
    private readonly ILogger<GetDriverDetailsQueryHandler> _logger;

    public GetDriverDetailsQueryHandler(IDriverRepository driverRepository,
        ILogger<GetDriverDetailsQueryHandler> logger)
    {
        _driverRepository = driverRepository;
        _logger = logger;
    }

    public async Task<DriverResponseDto> Handle(GetDriverDetailsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting driver info");
        var driver = await _driverRepository.GetByIdAsync(request.DriverId, cancellationToken);
        if (driver == null) throw new KeyNotFoundException("Driver not found");

        return DriverResponseDto.Create(driver.Id, driver.FullName.FirstName, driver.Email.Value,
            driver.Status.ToString(), driver.Fare.Amount);
    }
}