using SmartStay.Application.Dto.BookingDto;
using SmartStay.Application.Exceptions.BookingExceptions;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Booking,RoomAvailabilityDto> _mapper;

    public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, IMapper<Booking,RoomAvailabilityDto> mapper)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingRequestDto dto)
    {
        ValidateDates(dto.CheckIn, dto.CheckOut);

        var room = await _roomRepository.GetByIdAsync(dto.RoomId)
            ?? throw new KeyNotFoundException($"Room '{dto.RoomId}' not found.");

        var hasOverlap = await _bookingRepository.HasOverlapAsync(dto.RoomId, dto.CheckIn, dto.CheckOut);
        if (hasOverlap)
            throw new RoomNotAvailableException(dto.CheckIn, dto.CheckOut);

        int nights = (int)(dto.CheckOut - dto.CheckIn).TotalDays;

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            CheckinDate = dto.CheckIn,
            CheckOutDate = dto.CheckOut,
            TotalPrice = (decimal)room.PricePerNight * nights,
            Status = BookingStatus.Confirmed
        };

        await _bookingRepository.AddAsync(booking);

        return ToDto(booking, room.Name);
    }

    public async Task<BookingResponseDto> GetBookingAsync(Guid id)
    {
        var booking = await _bookingRepository.GetWithDetailsAsync(id)
            ?? throw new BookingNotFoundException(id);

        return ToDto(booking, booking.Room.Name);
    }

    public async Task<IEnumerable<BookingResponseDto>> GetUserBookingsAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return bookings.Select(b => ToDto(b, b.Room.Name));
    }

    public async Task<BookingResponseDto> CancelBookingAsync(Guid id, CancelBookingRequestDto dto)
    {
        var booking = await _bookingRepository.GetWithDetailsAsync(id)
            ?? throw new BookingNotFoundException(id);

        if (booking.Status == BookingStatus.Cancelled)
            throw new BookingCancellationException("Booking is already cancelled.");

        if (booking.Status == BookingStatus.CheckedIn || booking.Status == BookingStatus.CheckedOut)
            throw new BookingCancellationException($"Cannot cancel a booking with status '{booking.Status}'.");

        var log = new CancellationLog
        {
            Id = Guid.NewGuid(),
            BookingId = booking.Id,
            CancelledAt = DateTimeOffset.UtcNow,
            DaysBeforeCheckin = Math.Max(0, (int)(booking.CheckinDate - DateTimeOffset.UtcNow).TotalDays),
            Reason = dto.Reason
        };

        booking.Status = BookingStatus.Cancelled;
        await _bookingRepository.CancelAsync(booking, log);

        return ToDto(booking, booking.Room.Name);
    }

    private static void ValidateDates(DateTimeOffset checkIn, DateTimeOffset checkOut)
    {
        if (checkIn < DateTimeOffset.UtcNow)
            throw new InvalidBookingDatesException("Check-in date cannot be in the past.");

        if (checkOut <= checkIn)
            throw new InvalidBookingDatesException("Check-out must be after check-in.");

        if ((checkOut - checkIn).TotalDays < 1)
            throw new InvalidBookingDatesException("Minimum stay is 1 night.");
    }

    public async Task<IEnumerable<RoomAvailabilityDto>> GetOccupiedRooms(Guid roomId)
    {
        var bookings= await _bookingRepository.GetRoomOccupancyByRoomIdAsync(roomId);

        return bookings.Select(b => _mapper.ToDto(b));
    }

    private static BookingResponseDto ToDto(Booking b, string roomName) => new(
        b.Id,
        b.UserId,
        b.RoomId,
        roomName,
        b.CheckinDate,
        b.CheckOutDate,
        (int)(b.CheckOutDate - b.CheckinDate).TotalDays,
        b.TotalPrice,
        b.Status,
        b.CreatedAt
    );
}
