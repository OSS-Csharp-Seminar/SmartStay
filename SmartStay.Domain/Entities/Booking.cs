using SmartStay.Domain.Enums;
using System;

namespace SmartStay.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Room Room { get; set; }
        public DateTimeOffset CheckinDate { get; set; }         
        public DateTimeOffset CheckOutDate {get; set;}
        public float TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }= DateTimeOffset.UtcNow;

    }
}
