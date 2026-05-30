using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class RoomAmenityConfiguration : IEntityTypeConfiguration<RoomAmenity>
{
    public void Configure(EntityTypeBuilder<RoomAmenity> builder)
    {
        builder.ToTable("room_amenities");

       
        builder.HasKey(ra => new { ra.RoomId, ra.AmenityId });

        builder.HasOne(ra => ra.Room)
            .WithMany(r => r.RoomAmenities)
            .HasForeignKey(ra => ra.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ra => ra.Amenity)
            .WithMany(a => a.RoomAmenities)
            .HasForeignKey(ra => ra.AmenityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
           new RoomAmenity
           {
              RoomId =Guid.Parse("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
             AmenityId = Guid.Parse("44fd1192-34ea-4a44-b8c3-f2716cd744e6")
           },
           new RoomAmenity
           {
               RoomId =Guid.Parse("44d360a3-7433-4405-aaf2-32c2a3eebdf5"),
               AmenityId = Guid.Parse("48fd1822-33ea-4a44-b8e3-f2716dd744e6")
           },
           new RoomAmenity
           {
               RoomId =Guid.Parse("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
               AmenityId = Guid.Parse("48fd1893-3344-4a45-b7c3-f271acd744e6")
           },
           new RoomAmenity
           {
               RoomId =Guid.Parse("a6a7acdf-88c2-4fd2-b12e-a387d689f3db"),
               AmenityId = Guid.Parse("48fd1813-3344-4a45-42c2-f2718cd741e5")
           }
           );
    }
}