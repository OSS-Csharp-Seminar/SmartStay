namespace SmartStay.Domain.Entities;

public class RoomImage
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public string FileName { get; set; }
    
    public Room Room { get; set; }
}