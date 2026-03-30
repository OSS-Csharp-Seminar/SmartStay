using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStay.Domain.Entities
{
    public class CancellationLog
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
        
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CancelledAt { get; set; } = DateTimeOffset.UtcNow;
        public int DaysBeforeCheckin { get; set; }
        public string? Reason { get; set; }
    }
}
