using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rating)
            .IsRequired();
        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(r => r.CreatedAt)
            .HasDefaultValueSql("now()");
        
        builder.HasIndex(r => new {r.UserId, r.RoomId})
            .IsUnique();
        
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(r => r.Room)
            .WithMany(r => r.Reviews)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}