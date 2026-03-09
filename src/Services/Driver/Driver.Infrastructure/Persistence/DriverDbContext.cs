using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Persistence;

public sealed class DriverDbContext : DbContext
{
    public DriverDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Driver> Drivers => Set<Domain.Entities.Driver>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriverDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesWithDomainEventAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken);
    }
}