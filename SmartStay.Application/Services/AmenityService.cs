using SmartStay.Application.Dto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class AmenityService : IAmenityService
{
    
    private readonly IAmenitiesRepository _amenitiesRepository;
    private readonly IMapper<Amenity,AmenityResponseDto> _mapper;

    public AmenityService(IAmenitiesRepository amenitiesRepository, IMapper<Amenity,AmenityResponseDto> mapper)
    {
        _amenitiesRepository = amenitiesRepository;
        _mapper = mapper;
    }
        
    public async Task<IEnumerable<AmenityResponseDto>> GetAmenitiesAsync()
    {
        var amenities = await _amenitiesRepository.GetAllAsync();

        return amenities.Select(a => _mapper.ToDto(a)).ToList();
    }
}