namespace SmartStay.Application.Dto.BookingDto;

public record RoomAvailabilityDto(
   DateTimeOffset CheckIn,
   DateTimeOffset CheckOut
    );