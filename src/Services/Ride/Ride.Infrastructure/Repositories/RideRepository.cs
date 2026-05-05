using Microsoft.EntityFrameworkCore;
using Ride.Domain.Repositories;
using Ride.Infrastructure.Persistence;

namespace Ride.Infrastructure.Repositories;

public class RideRepository : IRideRepository
{
    private readonly RideDbContext _dbContext;

    public RideRepository(RideDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Ride?> GetByIdAsync(Guid id, CancellationToken cancellation)
    {
        return await _dbContext.Rides.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync(cancellation);
    }

    public Task<List<Domain.Entities.Ride>> GetAllRides(CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Domain.Entities.Ride ride, CancellationToken cancellation)
    {
        await _dbContext.AddAsync(ride, cancellation);
    }
}