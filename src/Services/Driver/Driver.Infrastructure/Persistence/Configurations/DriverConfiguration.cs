using Driver.Domain.Entities;
using Driver.Domain.Enums;
using Driver.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(x => x.FullName, fullName =>
        {
            fullName.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            fullName.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName("email")
                .IsRequired();

            email.Property(x => x.IsVerified)
                .HasColumnName("is_verified")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Fare, fare =>
        {
            fare.Property(x => x.Amount)
                .HasColumnName("fare_amount")
                .IsRequired();

            fare.Property(x => x.Currency)
                .HasColumnName("fare_currency")
                .HasConversion<string>()
                .IsRequired();
        });

        builder.HasOne(x => x.Document)
            .WithOne()
            .HasForeignKey<Document>("driver_id")
            .IsRequired(false);

        builder.HasOne(x => x.Vehicle)
            .WithOne()
            .HasForeignKey<Vehicle>("driver_id")
            .IsRequired(false);

        builder.Ignore("_domainEvents");
    }
}