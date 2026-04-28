using Location.Application.Abstraction;
using Location.Application.Dto;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Location.Infrastructure.Repository;

public class LocationRepository : ILocationRepository
{
    private const int DefaultRadius = 5;
    private static readonly RedisKey DriverGeoKey = "driver:locations";
    private static readonly RedisKey PassengerGeoKey = "passenger:locations";
    private readonly IDatabase _cache;
    private readonly ILogger<LocationRepository> _logger;

    public LocationRepository(ILogger<LocationRepository> logger, IConnectionMultiplexer redis)
    {
        _logger = logger;
        _cache = redis.GetDatabase();
    }

    public async Task UpdateDriverLocationAsync(Guid driverId, double latitude, double longitude)
    {
        await _cache.GeoAddAsync(DriverGeoKey, longitude, latitude, driverId.ToString());
    }

    public async Task UpdatePassengerLocationAsync(Guid passengerId, double latitude, double longitude)
    {
        await _cache.GeoAddAsync(PassengerGeoKey, longitude, latitude, passengerId.ToString());
    }

    public async Task<LocationDto?> GetDriverLocationAsync(Guid driverId)
    {
        var position = await _cache.GeoPositionAsync(DriverGeoKey, driverId.ToString());
        if (position is null) return null;

        return new LocationDto(position.Value.Latitude, position.Value.Longitude);
    }

    public async Task<LocationDto?> GetPassengerLocationAsync(Guid passengerId)
    {
        var position = await _cache.GeoPositionAsync(PassengerGeoKey, passengerId.ToString());
        if (position is null) return null;

        return new LocationDto(position.Value.Latitude, position.Value.Longitude);
    }

    public async Task RemoveDriverLocationAsync(Guid driverId)
    {
        if (await _cache.KeyExistsAsync(DriverGeoKey))
            await _cache.GeoRemoveAsync(DriverGeoKey, driverId.ToString());
    }

    public async Task RemovePassengerLocationAsync(Guid passengerId)
    {
        if (await _cache.KeyExistsAsync(PassengerGeoKey))
            await _cache.GeoRemoveAsync(PassengerGeoKey, passengerId.ToString());
    }

    public async Task<List<NearbyDriver>> GetNearestDriversAsync(double latitude, double longitude, double? radius)
    {
        var result = await _cache.GeoSearchAsync(DriverGeoKey, longitude, latitude,
            new GeoSearchBox(DefaultRadius, DefaultRadius, GeoUnit.Kilometers), 50, true,
            Order.Ascending, GeoRadiusOptions.WithDistance);

        if (result.Length == 0) return [];

        var nearest = result.Select(x =>
        {
            var distance = x.Distance ?? DefaultRadius;
            return new NearbyDriver(Guid.Parse(x.Member.ToString()), distance);
        }).ToList();

        return nearest;
    }
}