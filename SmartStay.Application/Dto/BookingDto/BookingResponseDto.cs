using SmartStay.Domain.Enums;

namespace SmartStay.Application.Dto.BookingDto;

public record BookingResponseDto(
    Guid Id,
    Guid UserId,
    Guid RoomId,
    string RoomName,
    DateTimeOffset CheckIn,
    DateTimeOffset CheckOut,
    int Nights,
    decimal TotalPrice,
    BookingStatus Status,
    DateTimeOffset CreatedAt
);
