using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
    public void Configure(EntityTypeBuilder<Amenity> builder)
    {
        builder.ToTable("amenities");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(a => a.Name)
            .IsUnique();

        builder.HasMany(a => a.RoomAmenities)
            .WithOne(A => A.Amenity)
            .HasForeignKey(a => a.AmenityId);

        builder.HasData(
            new Amenity
            {
                Id = Guid.Parse("44fd1192-34ea-4a44-b8c3-f2716cd744e6"),
                Name = "Wifi",
            },
        new Amenity
           {
               Id=Guid.Parse("48fd1822-33ea-4a44-b8e3-f2716dd744e6"),
               Name="Air conditioning",
           },
        new Amenity
        {
            Id=Guid.NewGuid(),
            Name="Pool",
        }, 
        new Amenity
            {
             Id=Guid.NewGuid(),
             Name="Garage",
            }, 
        new Amenity
        {
            Id=Guid.NewGuid(),
            Name="Gym",
        }, 
        new Amenity
        {
            Id=Guid.NewGuid(),
            Name="Spa",
        }, 
        new Amenity
        {
            Id=Guid.Parse("48fd1893-3344-4a45-b7c3-f271acd744e6"),
            Name="Jacuzzi",
        }, 
        new Amenity
        {
            Id=Guid.NewGuid(),
            Name="Breakfast",
        }, 
        new Amenity
        {
            Id=Guid.Parse("48fd1813-3344-4a45-42c2-f2718cd741e5"),
            Name="Fireplace",
        } 
            );
    }
}