using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class RoomImageConfiguration : IEntityTypeConfiguration<RoomImage>
{
    public void Configure(EntityTypeBuilder<RoomImage> builder)
    {
        builder.ToTable("RoomImages");
        
        
        builder.HasKey(ri => ri.Id);
        
        builder.Property(ri => ri.Id)
            .ValueGeneratedOnAdd();
        builder.Property(ri => ri.RoomId)
            .IsRequired();
        builder.Property(ri => ri.FileName)
            .IsRequired()
            .HasMaxLength(260);
        
       
    }
}