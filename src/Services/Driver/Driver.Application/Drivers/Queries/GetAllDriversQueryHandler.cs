using Driver.Application.Dtos;
using Driver.Application.Extensions;
using Driver.Domain.Repositories;
using MediatR;

namespace Driver.Application.Drivers.Queries;

public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, List<DriverResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDriversQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<DriverResponseDto>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.Drivers.GetAllDrivers(cancellationToken);

        return response.ToDtoList();
    }
}