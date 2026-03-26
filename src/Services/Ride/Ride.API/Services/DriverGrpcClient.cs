using Driver.API.Grpc;
using Grpc.Core;

namespace Ride.API.Services;

public interface IDriverGrpcClient
{
    Task<DriverInfoResponse?> GetDriverInfoAsync(string driverId);
    Task<bool> IsDriverAvailableAsync(string driverId);
}

public class DriverGrpcClient : IDriverGrpcClient
{
    private readonly DriverService.DriverServiceClient _client;
    private readonly ILogger<DriverGrpcClient> _logger;

    public DriverGrpcClient(DriverService.DriverServiceClient client, ILogger<DriverGrpcClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<DriverInfoResponse?> GetDriverInfoAsync(string driverId)
    {
        try
        {
            _logger.LogInformation("Calling Driver gRPC service to get info for driver: {DriverId}", driverId);

            var request = new GetDriverInfoRequest { DriverId = driverId };
            var response = await _client.GetDriverInfoAsync(request);

            return response;
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
}