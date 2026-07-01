using SmartStay.Application.Dto.BookingDto;
using SmartStay.Domain.Enums;

namespace SmartStay.Application.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(CreateBookingRequestDto dto);
    Task<BookingResponseDto> GetBookingAsync(Guid id);
    Task<IEnumerable<BookingResponseDto>> GetUserBookingsAsync(Guid userId);
    Task<IEnumerable<BookingResponseDto>> GetRenterBookingsAsync(Guid renterId);
    Task<BookingResponseDto> CancelBookingAsync(Guid id, CancelBookingRequestDto dto);
    Task<BookingResponseDto> MarkNoShowAsync(Guid bookingId);
    Task<BookingResponseDto> UpdatePaymentMethodAsync(Guid bookingId, PaymentMethod method);
    Task SyncStatusesAsync();
    Task<IEnumerable<RoomAvailabilityDto>> GetOccupiedRooms(Guid roomId);
}
