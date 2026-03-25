using SmartStay.Domain.Enums;
using System;

namespace SmartStay.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public float PricePerNight { get; set; }
        public int size { get; set; }
        public BedType BedType {  get; set; }
        public float AverageRating { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
