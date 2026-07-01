using SmartStay.Application.Dto.RoomDto;
using SmartStay.Domain.Dto;

namespace SmartStay.Application.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomResponseDto>> GetRooms(RoomQueryDto dto);
    
    Task<RoomResponseDto> GetRoomById(Guid id);

    Task<Guid> CreateRoom(RoomCreationDto dto);
    Task<IEnumerable<RoomResponseDto>> GetRenterRoomsAsync(Guid renterId);
    Task DeleteRoomAsync(Guid roomId, Guid renterId);
    Task UpdateRoomAsync(Guid roomId, Guid renterId, RoomUpdateDto dto);
}