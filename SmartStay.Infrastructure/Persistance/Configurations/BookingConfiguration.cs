using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class BookingConfiguration :  IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.TotalPrice)
            .HasColumnType("numeric(10,2)")
            .IsRequired();
        
        builder.Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("now()");
        builder.Property(b => b.CheckinDate)
            .HasColumnType("datetime")
            .IsRequired();
        builder.Property(b => b.CheckOutDate)
            .HasColumnType("datetime")
            .IsRequired();
        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b. Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}