using Driver.Domain.Enums;

namespace Driver.Domain.Repositories;

public interface IDriverRepository
{
    Task<Entities.Driver?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<Entities.Driver?> GetByEmailAsync(string email, CancellationToken cancellation = default);
    Task<List<Entities.Driver>> GetAllByStatusAsync(DriverStatus status, CancellationToken cancellation = default);
    Task CreateAsync(Entities.Driver driver, CancellationToken cancellation = default);
    Task<List<Entities.Driver>> GetAllDriversAsync(CancellationToken cancellation = default);
    Task<List<Entities.Driver>> GetDriversByIdsAsync(List<Guid> driverIds, CancellationToken cancellation = default);
}