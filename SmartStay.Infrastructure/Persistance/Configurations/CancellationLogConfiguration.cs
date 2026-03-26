using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class CancellationLogConfiguration : IEntityTypeConfiguration<CancellationLog>
{
    public void Configure(EntityTypeBuilder<CancellationLog> builder)
    {
        builder.ToTable("CancellationLog");
        builder.HasKey(r => r.Id);  
        
        builder.Property(c => c.CancelledAt)
            .HasDefaultValueSql("now()");

        builder.Property(c => c.DaysBeforeCheckin)
            .IsRequired();

        builder.Property(c => c.Reason)
            .HasMaxLength(1000);
        
        builder.HasOne(c => c.Booking)
            .WithOne(b => b.CancellationLog)
            .HasForeignKey<CancellationLog>(c => c.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}