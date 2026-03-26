using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Amount)
            .HasColumnType("numeric(10,2)")
            .IsRequired();
        builder.Property(p => p.PaymentMethod)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(p => p.PaymentStatus)
            .IsRequired()
            .HasConversion<string>();
        
        builder.Property(p => p.PaidAt);
        
        builder.HasOne(p => p.Booking)
            .WithOne(b => b.Payment)
            .HasForeignKey<Payment>(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}