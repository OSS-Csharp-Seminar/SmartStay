using SmartStay.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStay.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CheckinDate { get; set; }         
        
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CheckOutDate {get; set;}
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CreatedAt { get; set; }= DateTimeOffset.UtcNow;
        
        public Payment? Payment { get; set; }
        public CancellationLog?  CancellationLog { get; set; }

    }
}
