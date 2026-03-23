using System.Text.Json;
using Driver.Domain.Entities;
using Driver.Domain.Enums;
using Driver.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Driver.Infrastructure.Persistence.Configurations;

public sealed class DriverConfiguration : IEntityTypeConfiguration<Domain.Entities.Driver>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Driver> builder)
    {
        builder.ToTable("drivers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property<DriverStatus>("Status")
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property<FullName>("FullName")
            .HasColumnName("full_name")
            .HasConversion(JsonConverter<FullName>())
            .IsRequired();

        builder.Property<Email>("Email")
            .HasColumnName("email")
            .HasConversion(JsonConverter<Email>())
            .IsRequired();

        builder.Property<Document>("Document")
            .HasColumnName("document")
            .HasConversion(JsonConverter<Document>())
            .IsRequired(false);

        builder.Property<Vehicle>("Vehicle")
            .HasColumnName("vehicle")
            .HasConversion(JsonConverter<Vehicle>())
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