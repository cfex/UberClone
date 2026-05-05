using Grpc.Core;
using Microsoft.Extensions.Logging;
using Ride.Application.Abstractions;
using Ride.Application.Dto;
using Ride.Infrastructure.Grpc;

namespace Ride.Infrastructure.Services.gRPC;

public class DriverGrpcService : IDriverGrpcClient
{
    private readonly DriverService.DriverServiceClient _client;
    private readonly ILogger<DriverGrpcService> _logger;

    public DriverGrpcService(DriverService.DriverServiceClient client, ILogger<DriverGrpcService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<DriverInfoDto?> GetDriverInfoAsync(string driverId)
    {
        try
        {
            _logger.LogInformation("Calling Driver gRPC service to get info for driver: {DriverId}", driverId);

            var request = new GetDriverInfoRequest { DriverId = driverId };
            var response = await _client.GetDriverInfoAsync(request);

            return new DriverInfoDto(
                response.DriverId,
                response.Name,
                response.Status,
                response.Fare
            );
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "gRPC error while getting driver info: {Status}", ex.Status);
            return null;
        }
    }

    public async Task<bool> IsDriverAvailableAsync(string driverId)
    {
        try
        {
            _logger.LogInformation("Calling Driver gRPC service to check availability for driver: {DriverId}",
                driverId);

            var request = new IsDriverAvailableRequest { DriverId = driverId };
            var response = await _client.IsDriverAvailableAsync(request);

            return response.IsAvailable;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "gRPC error while checking driver availability: {Status}", ex.Status);
            return false;
        }
    }

    public async Task<List<DriverInfoDto>> GetAvailableDriversAsync(List<Guid> driverIds)
    {
        _logger.LogInformation("Calling Driver gRPC service to get all available drivers");

        var request = new AvailableDriversRequest { DriverId = { driverIds.Select(id => id.ToString()) } };
        var response = await _client.GetAvailableDriversAsync(request);

        return response.Drivers.Select(x => DriverInfoDto.Create(x.DriverId, x.Name, x.Status, x.Fare)).ToList();
    }
}