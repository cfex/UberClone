using Location.Application.Dto;

namespace Location.Application.Abstraction;

public interface ILocationRepository
{
    Task UpdateDriverLocationAsync(Guid driverId, double latitude, double longitude);
    Task UpdatePassengerLocationAsync(Guid passengerId, double latitude, double longitude);
    Task<LocationDto?> GetDriverLocationAsync(Guid driverId);
    Task<LocationDto?> GetPassengerLocationAsync(Guid passengerId);
    Task RemoveDriverLocationAsync(Guid driverId);
    Task RemovePassengerLocationAsync(Guid passengerId);
    Task<List<NearbyDriver>> GetNearestDriversAsync(double latitude, double longitude, double? radius = null);
}