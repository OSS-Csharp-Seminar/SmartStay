namespace SmartStay.Application.Dto.BookingDto;

public record CreateBookingRequestDto(
    Guid UserId,
    Guid RoomId,
    DateTimeOffset CheckIn,
    DateTimeOffset CheckOut
);
