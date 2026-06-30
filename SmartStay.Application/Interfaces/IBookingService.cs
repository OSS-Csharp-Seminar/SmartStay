using SmartStay.Application.Dto.BookingDto;

namespace SmartStay.Application.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(CreateBookingRequestDto dto);
    Task<BookingResponseDto> GetBookingAsync(Guid id);
    Task<IEnumerable<BookingResponseDto>> GetUserBookingsAsync(Guid userId);
    Task<IEnumerable<BookingResponseDto>> GetActiveUserBookingsAsync(Guid userId);   
    Task<IEnumerable<BookingResponseDto>> GetPreviousUserBookingsAsync(Guid userId); 
    Task<BookingResponseDto> CancelBookingAsync(Guid id, CancelBookingRequestDto dto);
    Task<IEnumerable<RoomAvailabilityDto>> GetOccupiedRooms(Guid roomId);
}