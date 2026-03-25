using SmartStay.Domain.Enums;
using System;

namespace SmartStay.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        
        public Guid BookingId {  get; set; }
        public Booking Booking { get; set; } = null!;
        
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTimeOffset? PaidAt { get; set; }
    }
}
