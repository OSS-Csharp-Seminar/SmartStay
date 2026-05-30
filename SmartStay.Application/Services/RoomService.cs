using SmartStay.Application.Dto.RoomDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Dto;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class RoomService : IRoomService
{
    
    private readonly IRoomRepository _repository;
    private readonly IMapper<Room,RoomResponseDto> _mapper;
    
    public RoomService(IRoomRepository repository, IMapper<Room,RoomResponseDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    
    
    public async Task<IEnumerable<RoomResponseDto>> GetRooms(RoomQueryDto dto)
    {
       var rooms= await _repository.GetAllRoomsByQueryAsync(dto);

       var dtos = rooms.Select(r => _mapper.ToDto(r)).ToList();

       return dtos;
    }

    public async Task<RoomResponseDto> GetRoomById(Guid id)
    {
       var room = await _repository.GetByIdAsync(id);
        
       return  _mapper.ToDto(room);
    }
}