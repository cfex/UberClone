using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ride.Domain.Enums;

namespace Ride.Infrastructure.Persistence.Configuration;

public class RideConfiguration : IEntityTypeConfiguration<Domain.Entities.Ride>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Ride> builder)
    {
        builder.ToTable("rides");
        builder.HasAlternateKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property<RideStatus>("Status")
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(x => x.PickupLocation, locationBuilder =>
        {
            locationBuilder.Property(l => l.Latitude)
                .HasColumnName("pickup_latitude")
                .IsRequired();

            locationBuilder.Property(l => l.Longitude)
                .HasColumnName("pickup_longitude")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Destination, locationBuilder =>
        {
            locationBuilder.Property(l => l.Latitude)
                .HasColumnName("destination_latitude")
                .IsRequired();

            locationBuilder.Property(l => l.Longitude)
                .HasColumnName("destination_longitude")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Amount)
                .HasColumnName("price")
                .HasDefaultValue(0.0)
                .IsRequired();

            priceBuilder.Property(p => p.Currency)
                .HasColumnName("currency")
                .HasConversion<string>()
                .HasDefaultValue(Currency.USD)
                .IsRequired();
        });

        builder.OwnsOne(x => x.ProposedPrice, proposedPriceBuilder =>
        {
            proposedPriceBuilder.Property(p => p.Amount)
                .HasColumnName("proposed_price")
                .HasDefaultValue(0.0)
                .IsRequired();

            proposedPriceBuilder.Property(p => p.Currency)
                .HasColumnName("proposed_currency")
                .HasConversion<string>()
                .HasDefaultValue(Currency.USD)
                .IsRequired();
        });

        builder.Ignore("_domainEvents");
    }

    private static ValueConverter<TValueObject, string> JsonConverter<TValueObject>()
    {
        return new ValueConverter<TValueObject, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<TValueObject>(v, (JsonSerializerOptions?)null)!);
    }
}