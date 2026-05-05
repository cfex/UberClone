using Ride.Application.Dto;

namespace Ride.Application.Abstractions;

public interface IDriverGrpcClient
{
    Task<DriverInfoDto?> GetDriverInfoAsync(string driverId);
    Task<bool> IsDriverAvailableAsync(string driverId);
    Task<List<DriverInfoDto>> GetAvailableDriversAsync(List<Guid> driverIds);
}