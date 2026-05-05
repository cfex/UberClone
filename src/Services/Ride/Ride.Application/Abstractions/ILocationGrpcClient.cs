using Ride.Application.Dto;

namespace Ride.Application.Abstractions;

public interface ILocationGrpcClient
{
    Task<List<NearestDriverResponseDto>> GetNearestDriversAsync(double latitude, double longitude, double radius);
    Task<LocationResponseDto?> GetPassengerLocationAsync(Guid passengerId);
    Task<LocationResponseDto?> GetDriverLocationAsync(Guid driverId);
}