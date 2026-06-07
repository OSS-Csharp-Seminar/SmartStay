using SmartStay.Application.Dto.BookingDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Mapper;

public class RoomAvailabilityMapper : IMapper<Booking,RoomAvailabilityDto>
{
    public RoomAvailabilityDto ToDto(Booking source)
    {
        return new RoomAvailabilityDto(source.CheckInDate, source.CheckOutDate);
    }

    public Booking ToSource(RoomAvailabilityDto destination)
    {
        throw new NotImplementedException();
    }
}