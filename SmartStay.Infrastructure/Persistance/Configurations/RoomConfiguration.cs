using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();
        
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

        builder.HasOne(r => r.Location)
            .WithMany()
            .HasForeignKey(r => r.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.RoomAmenities)
            .WithOne(ra => ra.Room)
            .HasForeignKey(ra => ra.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(r => r.Reviews)
            .WithOne(re => re.Room)
            .HasForeignKey(re => re.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Room
            {
                Id = Guid.Parse("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
                Name = "Deluxe Ocean View",
                Description = "Spacious room with stunning ocean views",
                Capacity = 2,
                PricePerNight = 250.00f,
                Size = 45,
                BedType = BedType.King,
                AverageRating = 4.8f,
                LocationId = Guid.Parse("44fd2891-34ea-4a44-b8c3-f2716cd744e6"),
                CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = Guid.Parse("487b0929-3600-450c-928a-5d0e9bcefaec"),
                Name = "Family Suite",
                Description = "Perfect for families, with two bedrooms and a living area",
                Capacity = 4,
                PricePerNight = 350.00f,
                Size = 75,
                BedType = BedType.Queen,
                AverageRating = 4.6f,
                LocationId = Guid.Parse("64a6ac13-7659-4434-9c39-403e98d7aa7e"),
                CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = Guid.Parse("cf5106c1-8fe1-4889-b905-c7810f2eb519"),
                Name = "Business Executive",
                Description = "Ideal for business travelers with work desk and high-speed internet",
                Capacity = 2,
                PricePerNight = 180.00f,
                Size = 30,
                BedType = BedType.Single,
                AverageRating = 4.4f,
                LocationId = Guid.Parse("d106dc17-6a8c-4e91-8355-9a1a756f7833"),
                CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = Guid.Parse("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
                Name = "Romantic Getaway",
                Description = "Cozy room perfect for couples with fireplace and jacuzzi",
                Capacity = 2,
                PricePerNight = 300.00f,
                Size = 40,
                BedType = BedType.King,
                AverageRating = 4.9f,
                LocationId = Guid.Parse("ebbff419-195d-4b57-af48-fac84d93f482"),
                CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = Guid.Parse("097758cd-3def-4b95-8d79-491a56f818b9"),
                Name = "Studio Apartment",
                Description = "Compact yet comfortable studio with kitchenette",
                Capacity = 2,
                PricePerNight = 120.00f,
                Size = 25,
                BedType = BedType.Single,
                AverageRating = 4.2f,
                LocationId = Guid.Parse("f6aa2e4e-64b6-4608-b73d-89cb699f0382"),
                CreatedAt = DateTime.UtcNow
            } 
        );
    }
}