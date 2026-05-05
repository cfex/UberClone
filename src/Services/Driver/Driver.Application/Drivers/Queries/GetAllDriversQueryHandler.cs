using Driver.Application.Dto;
using Driver.Application.Extensions;
using Driver.Domain.Repositories;
using MediatR;

namespace Driver.Application.Drivers.Queries;

public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, List<DriverResponseDto>>
{
    private readonly IDriverRepository _driverRepository;

    public GetAllDriversQueryHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<List<DriverResponseDto>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        var response = await _driverRepository.GetAllDriversAsync(cancellationToken);

        return response.ToDtoList();
    }
}