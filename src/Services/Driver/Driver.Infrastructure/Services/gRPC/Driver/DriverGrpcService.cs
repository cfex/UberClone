using Driver.Domain.Enums;
using Driver.Domain.Repositories;
using Driver.Domain.ValueObjects;
using Driver.Infrastructure.Grpc;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using DriverService = Driver.Infrastructure.Grpc.DriverService;
using GetDriverInfoRequest = Driver.Infrastructure.Grpc.GetDriverInfoRequest;
using IsDriverAvailableRequest = Driver.Infrastructure.Grpc.IsDriverAvailableRequest;
using IsDriverAvailableResponse = Driver.Infrastructure.Grpc.IsDriverAvailableResponse;

namespace Driver.Infrastructure.Services.gRPC.Driver;

public class DriverGrpcService : DriverService.DriverServiceBase
{
    private readonly IDriverRepository _driverRepository;
    private readonly ILogger<DriverGrpcService> _logger;

    public DriverGrpcService(ILogger<DriverGrpcService> logger, IDriverRepository driverRepository)
    {
        _logger = logger;
        _driverRepository = driverRepository;
    }

    public override async Task<DriverResponse> GetDriverInfo(
        GetDriverInfoRequest request,
        ServerCallContext context)
    {
        var driver = await _driverRepository.GetByIdAsync(Guid.Parse(request.DriverId));
        if (driver == null) throw new RpcException(new Status(StatusCode.NotFound, "Driver not found"));

        return new DriverResponse
        {
            DriverId = driver.Id.ToString(),
            Name = driver.FullName.FirstName,
            Status = driver.Status.ToString(),
            Fare = driver.Fare.Amount
        };
    }

    public override async Task<IsDriverAvailableResponse> IsDriverAvailable(
        IsDriverAvailableRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("Checking availability for driver: {DriverId}", request.DriverId);

        var driver = await _driverRepository.GetByIdAsync(Guid.Parse(request.DriverId));
        if (driver == null) throw new RpcException(new Status(StatusCode.NotFound, "Driver not found"));

        var response = new IsDriverAvailableResponse
        {
            IsAvailable = driver.Status == DriverStatus.Available
        };

        return response;
    }

    public override async Task<AvailableDriversList> GetAvailableDrivers(
        AvailableDriversRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("Getting available drivers near location: ({Latitude}, {Longitude})",
            request.Latitude, request.Longitude);

        var passengerLocation = Location.Create(request.Longitude, request.Latitude);

        var drivers = await _driverRepository.GetAvailableDriversInArea(passengerLocation);

        var response = new AvailableDriversList();

        foreach (var driver in drivers)
            response.Drivers.Add(new DriverResponse
            {
                DriverId = driver.Id.ToString(),
                Name = driver.FullName.FirstName,
                Email = driver.Email.Value,
                Status = driver.Status.ToString(),
                Fare = driver.Fare.Amount
            });

        return response;
    }
}