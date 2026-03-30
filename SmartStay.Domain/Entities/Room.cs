using SmartStay.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStay.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public float PricePerNight { get; set; }
        public int Size { get; set; }
        public BedType BedType {  get; set; }
        public float AverageRating { get; set; }
        
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        
        public ICollection<RoomAmenity> RoomAmenities { get; set; } = new List<RoomAmenity>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
    
}
