using System;

namespace SmartStay.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        
        public int Rating { get; set; } 
        public string Comment { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }= DateTimeOffset.UtcNow;
    }
}
