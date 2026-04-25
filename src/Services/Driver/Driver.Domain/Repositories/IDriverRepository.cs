using Driver.Domain.Enums;
using Driver.Domain.ValueObjects;

namespace Driver.Domain.Repositories;

public interface IDriverRepository
{
    Task<Entities.Driver?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<Entities.Driver?> GetByEmail(string email, CancellationToken cancellation = default);
    Task<List<Entities.Driver>> GetAllByStatus(DriverStatus status, CancellationToken cancellation = default);
    Task CreateAsync(Entities.Driver driver, CancellationToken cancellation = default);
    Task<List<Entities.Driver>> GetAllDrivers(CancellationToken cancellation = default);
    Task<List<Entities.Driver>> GetAvailableDriversInArea(Location area, CancellationToken cancellation = default);
}