using Driver.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ride.Application.Events;

namespace Ride.Infrastructure.Persistence;

public class RideDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public RideDbContext(DbContextOptions<RideDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Domain.Entities.Ride> Rides { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RideDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        var entities = ChangeTracker.Entries<AggregateRoot>().Select(e => e.Entity).ToList();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        foreach (var notification in from domainEvent in domainEvents
                 let notificationType = typeof(RideDomainEvent<>)
                     .MakeGenericType(domainEvent.GetType())
                 select Activator.CreateInstance(notificationType, domainEvent))
            await _publisher.Publish(notification, cancellationToken);

        entities.ForEach(entity => entity.ClearDomainEvents());

        return result;
    }
}