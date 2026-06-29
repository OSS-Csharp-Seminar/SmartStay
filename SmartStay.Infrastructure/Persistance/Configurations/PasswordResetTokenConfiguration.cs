using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("PasswordResetTokens");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Code).IsRequired().HasMaxLength(6);
        builder.Property(t => t.ExpiresAt).IsRequired();

        builder.HasOne(t => t.User)
            .WithOne()
            .HasForeignKey<PasswordResetToken>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
