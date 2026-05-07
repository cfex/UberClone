using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Passenger.Infrastructure.Persistance.Configurations;

public sealed class PassengerConfiguration : IEntityTypeConfiguration<Domain.Entities.Passenger>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Passenger> builder)
    {
        builder.ToTable("passengers");
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

        builder.Ignore("_domainEvents");
    }
}