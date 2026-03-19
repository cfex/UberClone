using Driver.Domain.Enums;
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
        return await _dbContext.Drivers
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellation);
    }

    public async Task<Domain.Entities.Driver?> GetByEmail(string email, CancellationToken cancellation = default)
    {
        return await _dbContext.Drivers
            .Where(x => x.Email.Equals(email))
            .FirstOrDefaultAsync(cancellation);
    }

    public async Task<List<Domain.Entities.Driver>> GetAllByStatus(DriverStatus status,
        CancellationToken cancellation = default)
    {
        return await _dbContext.Drivers
            .Where(x => x.Status == status)
            .AsNoTracking()
            .ToListAsync(cancellation);
    }

    public async Task CreateAsync(Domain.Entities.Driver driver, CancellationToken cancellation = default)
    {
        await _dbContext.Drivers.AddAsync(driver, cancellation);
    }

    public async Task<List<Domain.Entities.Driver>> GetAllDrivers(CancellationToken cancellation = default)
    {
        return await _dbContext.Drivers.AsNoTracking().ToListAsync(cancellation);
    }
}