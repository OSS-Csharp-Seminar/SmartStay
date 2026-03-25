using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance;

public class SmartStayDbContext : DbContext
{
    public SmartStayDbContext(DbContextOptions<SmartStayDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Amenity> Amenities => Set<Amenity>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<RoomAmenity> RoomAmenities => Set<RoomAmenity>();
    public DbSet<CancellationLog> CancellationLogs => Set<CancellationLog>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartStayDbContext).Assembly);
    }
}

