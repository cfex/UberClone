using Ride.Application.Dto;
using Ride.Domain.ValueObjects;

namespace Ride.Application.Abstractions;

public interface IDriverGrpcClient
{
    Task<DriverInfoDto?> GetDriverInfoAsync(string driverId);
    Task<bool> IsDriverAvailableAsync(string driverId);
    Task<List<DriverInfoDto>> GetAvailableDriversAsync(Location location);
}