using Driver.Domain.Entities;
using Driver.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driver.Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("documents");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .IsRequired();
        
      
        builder.Property(d => d.DocumentType)
            .HasColumnName("type")
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(d => d.ExpiryDate)
            .HasColumnName("expiry_date")
            .IsRequired();
        
        builder.Property<Guid>("driver_id");
    }
}