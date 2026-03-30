using SmartStay.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset? PaidAt { get; set; }
    }
}
