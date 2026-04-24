using Driver.Application.Dto;
using Driver.Application.Extensions;
using Driver.Domain.Repositories;
using MediatR;

namespace Driver.Application.Drivers.Queries;

public class GetDriverByIdQueryHandler
    : IRequestHandler<GetDriverByIdQuery, DriverResponseDto>
{
    private readonly IDriverRepository _driverRepository;

    public GetDriverByIdQueryHandler(IUnitOfWork unitOfWork, IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<DriverResponseDto> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(request.DriverId, cancellationToken);

        if (driver == null)
            throw new KeyNotFoundException($"Driver with ID {request.DriverId} not found");

        return driver.ToDto();
    }
}