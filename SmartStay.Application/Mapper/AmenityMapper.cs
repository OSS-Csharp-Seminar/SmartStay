using SmartStay.Application.Dto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Mapper;

public class AmenityMapper : IMapper<Amenity,AmenityResponseDto>
{
    public AmenityResponseDto ToDto(Amenity source)
    {
        return new  AmenityResponseDto(source.Id, source.Name);
    }

    public Amenity ToSource(AmenityResponseDto destination)
    {
        throw new NotImplementedException();
    }
}