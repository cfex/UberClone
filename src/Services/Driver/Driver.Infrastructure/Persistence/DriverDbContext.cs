using Driver.Application.Events;
using Driver.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Persistence;

public sealed class DriverDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public DriverDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Domain.Entities.Driver> Drivers => Set<Domain.Entities.Driver>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriverDbContext).Assembly);
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
                 let notificationType = typeof(DriverDomainEvent<>)
                     .MakeGenericType(domainEvent.GetType())
                 select Activator.CreateInstance(notificationType, domainEvent))
            await _publisher.Publish(notification, cancellationToken);

        entities.ForEach(entity => entity.ClearDomainEvents());

        return result;
    }
}