using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(r => r.Description)
            .HasMaxLength(2000);
        builder.Property(r => r.Capacity)
            .IsRequired();
        builder.Property(r => r.PricePerNight)
            .IsRequired()
            .HasColumnType("numeric(10,2)");
        builder.Property(r => r.Size)
            .IsRequired();
        builder.Property(r => r.BedType)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(r => r.AverageRating)
            .HasColumnType("numeric(3,2)")
            .HasDefaultValue(0.0m);
        builder.Property(r => r.CreatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("now()");
    }
}