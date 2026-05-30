using SmartStay.Application.Dto.RoomDto;
using SmartStay.Domain.Dto;

namespace SmartStay.Application.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomResponseDto>> GetRooms(RoomQueryDto dto);
    
    Task<RoomResponseDto> GetRoomById(Guid id);
}