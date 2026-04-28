using Grpc.Core;
using Location.Application.Abstraction;
using Location.Infrastructure.Grpc;
using Microsoft.Extensions.Logging;

namespace Location.Infrastructure.Services.Grpc.Location;

public class LocationGrpcService : LocationService.LocationServiceBase
{
    private readonly ILogger<LocationGrpcService> _logger;
    private readonly ILocationRepository _repository;

    public LocationGrpcService(ILogger<LocationGrpcService> logger, ILocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public override async Task<NearestDriversResponse> GetNearestAvailableDrivers(NearestDriversRequest request,
        ServerCallContext context)
    {
        var drivers = await _repository.GetNearestDriversAsync(request.Latitude, request.Longitude, request.Radius);
        var nearest = new NearestDriversResponse();

        foreach (var driver in drivers)
            nearest.Drivers.Add(new DriverLocation
            {
                DriverId = driver.DriverId.ToString(),
                Distance = driver.Distance
            });

        return nearest;
    }

    public override async Task<LocationResponse> GetPassengerLocation(GetLocationRequest request,
        ServerCallContext context)
    {
        var location = await _repository.GetPassengerLocationAsync(Guid.Parse(request.EntityId));
        if (location == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Location with id {request.EntityId} not found"));

        return new LocationResponse { Latitude = location.Latitude, Longitude = location.Longitude };
    }

    public override async Task<LocationResponse> GetDriverLocation(GetLocationRequest request,
        ServerCallContext context)
    {
        var location = await _repository.GetDriverLocationAsync(Guid.Parse(request.EntityId));
        if (location == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Location with id {request.EntityId} not found"));

        return new LocationResponse { Latitude = location.Latitude, Longitude = location.Longitude };
    }
}