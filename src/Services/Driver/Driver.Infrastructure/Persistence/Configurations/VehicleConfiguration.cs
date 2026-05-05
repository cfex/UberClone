using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driver.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("vehicle_id")
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(x => x.Make)
            .HasColumnName("make")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Model)
            .HasColumnName("model")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Color)
            .HasColumnName("color")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LicensePlate)
            .HasColumnName("license_plate")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.RegistrationDate)
            .HasColumnName("registration_date")
            .HasColumnType("date");
        
        builder.Property<Guid>("driver_id");
    }
}