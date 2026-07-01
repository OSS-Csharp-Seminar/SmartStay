using SmartStay.Domain.Dto;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;

namespace SmartStay.Domain.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    
    Task<IEnumerable<Room>> GetAllRoomsByQueryAsync(RoomQueryDto dto);
    Task<IEnumerable<Room>> GetByRenterIdAsync(Guid renterId);
}