using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Driver.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DriverDbContext>
{
    public DriverDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DriverDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5433;Database=driver_db;Username=driver;Password=postgres"
        );

        return new DriverDbContext(optionsBuilder.Options, new NoOpPublisher());
    }

    private class NoOpPublisher : IPublisher
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            return Task.CompletedTask;
        }
    }
}