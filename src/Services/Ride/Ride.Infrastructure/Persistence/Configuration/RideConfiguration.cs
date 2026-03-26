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

        builder.Property(x => x.PickupLocation)
            .HasColumnName("pickup_location")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Destination)
            .HasColumnName("destination")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasConversion<double>()
            .HasDefaultValue(0.0)
            .IsRequired();

        builder.Property(x => x.ProposedPrice)
            .HasColumnName("proposed_price")
            .HasConversion<double>()
            .IsRequired();

        builder.Ignore("_domainEvents");
    }

    private static ValueConverter<TValueObject, string> JsonConverter<TValueObject>()
    {
        return new ValueConverter<TValueObject, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<TValueObject>(v, (JsonSerializerOptions?)null)!);
    }
}