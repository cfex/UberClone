using Grpc.Core;
using Microsoft.Extensions.Logging;
using Ride.Application.Abstractions;
using Ride.Application.Dto;
using Ride.Infrastructure.Grpc;

namespace Ride.Infrastructure.Services.gRPC;

public class LocationGrpcService : ILocationGrpcClient
{
    private readonly LocationService.LocationServiceClient _client;
    private readonly ILogger<LocationGrpcService> _logger;

    public LocationGrpcService(ILogger<LocationGrpcService> logger, LocationService.LocationServiceClient client)
    {
        _client = client;
        _logger = logger;
    }

    // TODO: this is the same function (for now)
    public async Task<LocationResponseDto?> GetPassengerLocationAsync(Guid passengerId)
    {
        _logger.LogInformation("Calling GetPassengerLocationAsync");

        try
        {
            var request = new GetLocationRequest { EntityId = passengerId.ToString() };
            var response = await _client.GetPassengerLocationAsync(request);

            return LocationResponseDto.Create(response.Latitude, response.Longitude);
        }
        catch (RpcException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<LocationResponseDto?> GetDriverLocationAsync(Guid driverId)
    {
        _logger.LogInformation("Calling GetDriverLocationAsync");

        try
        {
            var request = new GetLocationRequest { EntityId = driverId.ToString() };
            var response = await _client.GetDriverLocationAsync(request);

            return LocationResponseDto.Create(response.Latitude, response.Longitude);
        }
        catch (RpcException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<List<NearestDriverResponseDto>> GetNearestDriversAsync(double latitude, double longitude,
        double radius)
    {
        _logger.LogInformation("Calling GetNearestDriversAsync");

        try
        {
            var request = new NearestDriversRequest { Latitude = latitude, Longitude = longitude, Radius = radius };
            var response = await _client.GetNearestAvailableDriversAsync(request);

            return response.Drivers.Select(x => NearestDriverResponseDto.Create(Guid.Parse(x.DriverId), x.Distance))
                .ToList();
        }
        catch (RpcException e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }
}