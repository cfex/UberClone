namespace Passenger.Domain.Repositories;

public interface IPassengerRespository
{
    Task<Entities.Passenger?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}