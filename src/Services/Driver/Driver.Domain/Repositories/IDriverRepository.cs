namespace Driver.Domain.Repositories;

public interface IDriverRepository
{
    Task<Entities.Driver?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task CreateAsync(Entities.Driver driver, CancellationToken cancellation = default);

    Task<List<Entities.Driver>> GetAllDrivers(CancellationToken cancellation = default);
}