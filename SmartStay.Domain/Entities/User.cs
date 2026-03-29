using SmartStay.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStay.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } =  string.Empty;
        public string FirstName { get; set; } =  string.Empty;
        public string LastName { get; set; } =   string.Empty;
        public Role Role {  get; set; }
        
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsActive {  get; set; }
        
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
