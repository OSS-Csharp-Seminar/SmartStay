using System;

namespace SmartStay.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public User User {  get; set; }
        public Room Room {  get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; }
        public DateTimeOffset CreatedAt { get; set; }= DateTimeOffset.UtcNow;
    }
}
