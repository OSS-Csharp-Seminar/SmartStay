using SmartStay.Application.Dto.RoomDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Dto;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class RoomService : IRoomService
{
    
    private readonly IRoomRepository _repository;
    private readonly IMapper<Room,RoomResponseDto> _RoomResponseMapper;
    private readonly IMapper<Room,RoomCreationDto> _RoomCreationMapper;
    
    public RoomService(IRoomRepository repository, IMapper<Room,RoomResponseDto> roomResponseMapper, IMapper<Room,RoomCreationDto> roomCreationMapper)
    {
        _repository = repository;
        _RoomResponseMapper = roomResponseMapper;
        _RoomCreationMapper = roomCreationMapper;
    }
    
    
    
    public async Task<IEnumerable<RoomResponseDto>> GetRooms(RoomQueryDto dto)
    {
       var rooms= await _repository.GetAllRoomsByQueryAsync(dto);

       var dtos = rooms.Select(r => _RoomResponseMapper.ToDto(r)).ToList();

       return dtos;
    }

    public async Task<RoomResponseDto> GetRoomById(Guid id)
    {
       var room = await _repository.GetByIdAsync(id);
        
       return  _RoomResponseMapper.ToDto(room);
    }

    public async Task<Guid> CreateRoom(RoomCreationDto dto)
    {
        var room = _RoomCreationMapper.ToSource(dto);
        var newRoom = await _repository.AddAsync(room);

        return newRoom.Id;
    }

    public async Task<IEnumerable<RoomResponseDto>> GetRenterRoomsAsync(Guid renterId)
    {
        var rooms = await _repository.GetByRenterIdAsync(renterId);
        return rooms.Select(r => _RoomResponseMapper.ToDto(r));
    }

    public async Task DeleteRoomAsync(Guid roomId, Guid renterId)
    {
        var room = await _repository.GetByIdAsync(roomId)
            ?? throw new KeyNotFoundException("Room not found.");
        if (room.RenterId != renterId)
            throw new UnauthorizedAccessException("You don't own this room.");
        await _repository.DeleteAsync(room);
    }

    public async Task UpdateRoomAsync(Guid roomId, Guid renterId, RoomUpdateDto dto)
    {
        var room = await _repository.GetByIdAsync(roomId)
            ?? throw new KeyNotFoundException("Room not found.");
        if (room.RenterId != renterId)
            throw new UnauthorizedAccessException("You don't own this room.");

        room.Name = dto.Name;
        room.Description = dto.Description;
        room.Capacity = dto.Capacity;
        room.PricePerNight = dto.PricePerNight;
        room.Size = dto.Size;
        room.BedType = dto.BedType;

        await _repository.UpdateAsync(room);
    }
}