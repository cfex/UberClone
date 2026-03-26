using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ride.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RideDbContext>
{
    public RideDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RideDbContext>();
        
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5434;Database=ride_db;Username=ride;Password=postgres"
        );

        // Koristimo NoOpPublisher jer u design-time ne treba pravi MediatR
        return new RideDbContext(optionsBuilder.Options, new NoOpPublisher());
    }
    
    // Helper klasa koja ne radi ništa - samo za design-time
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
