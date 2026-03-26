using Driver.Application.Dtos;
using Driver.Domain.Repositories;
using MediatR;

namespace Driver.Application.Drivers.Commands.GetDriverInfo;

public class GetDriverInfoCommandHandler : IRequestHandler<GetDriverInfoCommand, DriverResponseDto>
{
    private readonly IDriverRepository _driverRepository;

    public GetDriverInfoCommandHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<DriverResponseDto> Handle(GetDriverInfoCommand request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(Guid.Parse(request.DriverId), cancellationToken);
        if (driver == null) throw new KeyNotFoundException("Driver not found");

        return DriverResponseDto.Create(driver.Id, driver.FullName.FirstName, driver.Email.Value);
    }
}