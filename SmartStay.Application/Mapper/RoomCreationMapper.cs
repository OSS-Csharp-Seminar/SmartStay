using SmartStay.Application.Dto.RoomDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Mapper;

public class RoomCreationMapper : IMapper<Room,RoomCreationDto>
{
    public RoomCreationDto ToDto(Room source)
    {
        throw new NotImplementedException();
    }

    public Room ToSource(RoomCreationDto destination)
    {
        return new Room
        {
           Name = destination.Name,
           Description = destination.Description,
           Capacity = destination.Capacity,
           PricePerNight =  destination.PricePerNight,
           Size = destination.Size,
           BedType =  destination.BedType,
           Location = new Location
           {
               Address = destination.Location.Address,
               City = destination.Location.City,
               Country = destination.Location.Country,
               PostalCode = destination.Location.PostalCode,
               Latitude = destination.Location.Latitude,
               Longitude = destination.Location.Longitude
           }
        };
    }
}