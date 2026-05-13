using Microsoft.EntityFrameworkCore;
using Passenger.Domain.Repositories;
using Passenger.Infrastructure.Persistance;

namespace Passenger.Infrastructure.Repositories;

public class PassengerRepository : IPassengerRespository
{
    private readonly PassengerDbContext _dbContext;

    public PassengerRepository(PassengerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Passenger?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Passengers
            .Where(x => x.Id.Equals(id))
            .FirstOrDefaultAsync(cancellationToken);
    }
}