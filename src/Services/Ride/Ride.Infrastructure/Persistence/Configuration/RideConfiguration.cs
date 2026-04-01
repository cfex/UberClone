using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ride.Domain.Enums;

namespace Ride.Infrastructure.Persistence.Configuration;

public class RideConfiguration : IEntityTypeConfiguration<Domain.Entities.Ride>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Ride> builder)
    {
        builder.ToTable("rides");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.DriverId)
            .HasColumnName("driver_id")
            .IsRequired(false);

        builder.Property(x => x.PassengerId)
            .HasColumnName("passenger_id")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder.Property(x => x.StartedAt)
            .HasColumnName("started_at")
            .IsRequired(false);

        builder.Property(x => x.CompletedAt)
            .HasColumnName("completed_at")
            .IsRequired(false);

        builder.OwnsOne(x => x.PickupLocation, loc =>
        {
            loc.Property(l => l.Latitude).HasColumnName("pickup_latitude").IsRequired();
            loc.Property(l => l.Longitude).HasColumnName("pickup_longitude").IsRequired();
        });

        builder.OwnsOne(x => x.Destination, loc =>
        {
            loc.Property(l => l.Latitude).HasColumnName("destination_latitude").IsRequired();
            loc.Property(l => l.Longitude).HasColumnName("destination_longitude").IsRequired();
        });

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .HasDefaultValue(0.0)
                .IsRequired();

            price.Property(p => p.Currency)
                .HasColumnName("price_currency")
                .HasConversion<string>()
                .HasDefaultValue(Currency.USD)
                .IsRequired();
        });

        builder.Ignore("_domainEvents");
    }
}