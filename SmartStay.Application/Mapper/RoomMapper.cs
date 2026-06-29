using SmartStay.Application.Dto;
using SmartStay.Application.Dto.RoomDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Mapper;

public class RoomMapper : IMapper<Room,RoomResponseDto>
{
    public RoomResponseDto ToDto(Room source)
    {
        return new RoomResponseDto(
            source.Id,
            source.Name,
            source.Description,
            source.Capacity,
            source.PricePerNight,
            source.Size,
            source.BedType,
            source.AverageRating,
            new RoomLocationDto(source.Location.Country
                ,source.Location.City
                ,source.Location.Address
                ,source.Location.Longitude
                ,source.Location.Latitude),
          source.RoomAmenities.Select(r => r.Amenity.Name).ToList(),
           source.RoomImages.Select(r => r.FileName).ToList()
        );
    }

    public Room ToSource(RoomResponseDto destination)
    {
        throw new NotImplementedException();
    }
}