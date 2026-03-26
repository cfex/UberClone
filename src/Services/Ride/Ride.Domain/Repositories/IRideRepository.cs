namespace Ride.Domain.Repositories;

public interface IRideRepository
{
    Task<Entities.Ride?> GetByIdAsync(Guid id, CancellationToken cancellation);
    Task<List<Entities.Ride>> GetAllRides(CancellationToken cancellation);
    Task CreateAsync(Entities.Ride ride, CancellationToken cancellation);
}