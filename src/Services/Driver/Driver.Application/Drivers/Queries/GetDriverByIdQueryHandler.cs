using Driver.Application.Dtos;
using Driver.Domain.Repositories;
using MediatR;

namespace Driver.Application.Drivers.Queries;

public class GetDriverByIdQueryHandler
    : IRequestHandler<GetDriverByIdQuery, DriverResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDriverByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DriverResponseDto> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(request.DriverId, cancellationToken);
        
        if (driver == null) 
            throw new KeyNotFoundException($"Driver with ID {request.DriverId} not found");

        return DriverResponseDto.Create(driver.Id, driver.FullName.FirstName, driver.Email.Value);
    }
}