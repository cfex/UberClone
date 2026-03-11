using Driver.Domain.Repositories;
using Driver.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Repositories;

public sealed class DriverRepository : IDriverRepository
{
    private readonly DriverDbContext _dbContext;

    public DriverRepository(DriverDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Driver?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        return await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id, cancellation);
    }

    public async Task CreateAsync(Domain.Entities.Driver driver, CancellationToken cancellation = default)
    {
        await _dbContext.Drivers.AddAsync(driver, cancellation);
    }

    public async Task<List<Domain.Entities.Driver>> GetAllDrivers(CancellationToken cancellation = default)
    {
        return await _dbContext.Drivers.ToListAsync(cancellation);
    }
}