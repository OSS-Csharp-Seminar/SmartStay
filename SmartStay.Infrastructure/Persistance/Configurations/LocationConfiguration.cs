using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
       builder.ToTable("Locations"); 
       
       builder.HasKey(l => l.Id);
       builder.Property(r => r.Id)
           .ValueGeneratedOnAdd();
        builder.Property(l => l.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(l => l.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(l => l.Address)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(l => l.PostalCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(l => l.Latitude)
            .IsRequired()
            .HasColumnType("decimal(9,6)");

        builder.Property(l => l.Longitude)
            .IsRequired()
            .HasColumnType("decimal(9,6)");

        builder.HasData(
            new Location
            {
                Id = Guid.Parse("44fd2891-34ea-4a44-b8c3-f2716cd744e6"),
                Country = "United States",
                City = "New York",
                Address = "123 Main Street",
                PostalCode = "10001",
                Latitude = 40.7128,
                Longitude = -74.0060
            },
            new Location
            {
                Id = Guid.Parse("64a6ac13-7659-4434-9c39-403e98d7aa7e"),
                Country = "United Kingdom",
                City = "London",
                Address = "456 Oxford Street",
                PostalCode = "SW1A 1AA",
                Latitude = 51.5074,
                Longitude = -0.1278
            },
            new Location
            {
                Id = Guid.Parse("d106dc17-6a8c-4e91-8355-9a1a756f7833"),
                Country = "France",
                City = "Paris",
                Address = "789 Champs-Élysées",
                PostalCode = "75008",
                Latitude = 48.8566,
                Longitude = 2.3522
            },
            new Location
            {
                Id = Guid.Parse("ebbff419-195d-4b57-af48-fac84d93f482"),
                Country = "Japan",
                City = "Tokyo",
                Address = "321 Shibuya Crossing",
                PostalCode = "150-0043",
                Latitude = 35.6762,
                Longitude = 139.6503
            },
            new Location
            {
                Id = Guid.Parse("f6aa2e4e-64b6-4608-b73d-89cb699f0382"),
                Country = "Australia",
                City = "Sydney",
                Address = "555 Harbour Bridge Road",
                PostalCode = "2000",
                Latitude = -33.8688,
                Longitude = 151.2093
            } 
        );
    }
}