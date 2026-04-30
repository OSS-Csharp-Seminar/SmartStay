using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;

namespace SmartStay.Infrastructure.Persistance.Migrations;

public interface ISmartStayDbContext
{
    DbSet<User> Users { get; }
    DbSet<Room> Rooms { get; }
    DbSet<Amenity> Amenities { get; }
    DbSet<Review> Reviews { get; }
    DbSet<Booking> Bookings { get; }
    DbSet<RoomAmenity> RoomAmenities { get; }
    DbSet<CancellationLog> CancellationLogs { get; }
    DbSet<Payment> Payments { get; }
}