using MediatR;
using Ride.Application.Abstractions;
using Ride.Application.Dto;

namespace Ride.Application.Rides.Queries;

public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, DriverInfoDto>
{
    private readonly IDriverGrpcClient _driverGrpcClient;

    public GetDriverQueryHandler(IDriverGrpcClient driverGrpcClient)
    {
        _driverGrpcClient = driverGrpcClient;
    }

    public async Task<DriverInfoDto> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var driver = await _driverGrpcClient.GetDriverInfoAsync(request.DriverId.ToString());

        if (driver is null)
            throw new Exception($"Driver {request.DriverId} not found");

        return DriverInfoDto.Create(driver.DriverId, driver.Name, driver.Status, driver.Fare);
    }
}