using System;

namespace SmartStay.Domain.Entities
{
    public class CancellationLog
    {
        public Guid Id { get; set; }
        public Booking Booking { get; set; }
        public DateTimeOffset CancelledAt { get; set; } = DateTimeOffset.UtcNow;
        public int DaysBeforeCheckin { get; set; }
        public string? Reason { get; set; }
    }
}
