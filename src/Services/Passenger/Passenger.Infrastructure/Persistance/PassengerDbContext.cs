using MediatR;
using Microsoft.EntityFrameworkCore;
using Passenger.Application.Events;
using Shared.Domain.Primitives;

namespace Passenger.Infrastructure.Persistance;

public class PassengerDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public PassengerDbContext(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public DbSet<Domain.Entities.Passenger> Passengers => Set<Domain.Entities.Passenger>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PassengerDbContext).Assembly);
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
                 let notificationType = typeof(PassengerDomaineEvent<>)
                     .MakeGenericType(domainEvent.GetType())
                 select Activator.CreateInstance(notificationType, domainEvent))
            await _publisher.Publish(notification, cancellationToken);

        entities.ForEach(entity => entity.ClearDomainEvents());

        return result;
    }
}