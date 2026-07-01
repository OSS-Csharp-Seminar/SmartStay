using System.Transactions;
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
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper<Booking,RoomAvailabilityDto> _mapper;
    private readonly ITransactionSecurity _transactionSecurity;

    public BookingService(IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IPaymentRepository paymentRepository,
        IMapper<Booking,RoomAvailabilityDto> mapper,
        ITransactionSecurity transactionSecurity)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
        _transactionSecurity = transactionSecurity;
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

        var bookingId = Guid.NewGuid();
        var totalPrice = (decimal)room.PricePerNight * nights;

        var payment = new Payment { BookingId = bookingId, Amount = totalPrice, PaymentStatus = PaymentStatus.Pending, PaymentMethod = PaymentMethod.NotChosen };

        var booking = new Booking
        {
            Id = bookingId,
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            CheckinDate = dto.CheckIn,
            CheckOutDate = dto.CheckOut,
            TotalPrice = totalPrice,
            Status = BookingStatus.Confirmed,
        };

        await _transactionSecurity.BeginTransactionAsync();
        try
        {
            await _bookingRepository.AddAsync(booking);
            await _paymentRepository.AddAsync(payment);

            await _transactionSecurity.SaveChangesAsync();
            await _transactionSecurity.CommitAsync();
        }
        catch
        {
            await _transactionSecurity.RollbackAsync();
            Console.WriteLine("Error: error with booking creation");
            throw new TransactionAbortedException("error with booking creation");
        }

        return ToDto(booking, room.Name,null);
    }

    public async Task<BookingResponseDto> GetBookingAsync(Guid id)
    {
        var booking = await _bookingRepository.GetWithDetailsAsync(id)
            ?? throw new BookingNotFoundException(id);

        return ToDto(booking, booking.Room.Name,null);
    }

    public async Task<IEnumerable<BookingResponseDto>> GetUserBookingsAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return bookings.Select(b => ToDto(b, b.Room.Name,b.Room.Renter.Email));
    }

    public async Task<IEnumerable<BookingResponseDto>> GetRenterBookingsAsync(Guid renterId)
    {
        var bookings = await _bookingRepository.GetByRenterIdAsync(renterId);
        return bookings.Select(b => ToDto(b, b.Room.Name,b.User.Email));
    }

    public async Task<BookingResponseDto> CancelBookingAsync(Guid id, CancelBookingRequestDto dto)
    {
        var booking = await _bookingRepository.GetWithDetailsAsync(id)
            ?? throw new BookingNotFoundException(id);

        if (booking.Status == BookingStatus.Cancelled)
            throw new BookingCancellationException("Booking is already cancelled.");

        if (booking.Status == BookingStatus.CheckedIn || booking.Status == BookingStatus.CheckedOut)
            throw new BookingCancellationException($"Cannot cancel a booking with status '{booking.Status}'.");

        var daysUntilCheckin = (booking.CheckinDate - DateTimeOffset.UtcNow).TotalDays;
        if (daysUntilCheckin < 3)
            throw new BookingCancellationException("Bookings can only be cancelled at least 3 days before check-in.");

        var log = new CancellationLog
        {
            Id = Guid.NewGuid(),
            BookingId = booking.Id,
            CancelledAt = DateTimeOffset.UtcNow,
            DaysBeforeCheckin = Math.Max(0, (int)daysUntilCheckin),
            Reason = dto.Reason
        };

        booking.Status = BookingStatus.Cancelled;
        await _bookingRepository.CancelAsync(booking, log);

        return ToDto(booking, booking.Room.Name,null);
    }

    public async Task<BookingResponseDto> MarkNoShowAsync(Guid bookingId)
    {
        var booking = await _bookingRepository.GetWithDetailsAsync(bookingId)
            ?? throw new BookingNotFoundException(bookingId);

        if (booking.Status != BookingStatus.Confirmed)
            throw new BookingCancellationException($"Cannot mark no-show for booking with status '{booking.Status}'.");

        if (booking.CheckinDate > DateTimeOffset.UtcNow)
            throw new BookingCancellationException("Cannot mark no-show before check-in date.");

        booking.Status = BookingStatus.NotShowed;
        await _bookingRepository.UpdateAsync(booking);

        return ToDto(booking, booking.Room.Name,null);
    }

    public async Task<BookingResponseDto> UpdatePaymentMethodAsync(Guid bookingId, PaymentMethod method)
    {
        var booking = await _bookingRepository.GetWithDetailsAsync(bookingId)
            ?? throw new BookingNotFoundException(bookingId);

        if (booking.Status != BookingStatus.Confirmed)
            throw new InvalidOperationException("Payment method can only be changed for confirmed bookings.");

        if (booking.CheckinDate <= DateTimeOffset.UtcNow)
            throw new InvalidOperationException("Cannot change payment method after check-in date.");

        var payment = await _paymentRepository.GetByBookingIdAsync(bookingId)
            ?? throw new KeyNotFoundException($"Payment for booking '{bookingId}' not found.");

        payment.PaymentMethod = method;
        await _paymentRepository.UpdateAsync(payment);

        return ToDto(booking, booking.Room.Name,null);
    }

    public async Task SyncStatusesAsync()
    {
        var bookings = await _bookingRepository.GetAllActiveForSyncAsync();
        var now = DateTimeOffset.UtcNow;

        foreach (var booking in bookings)
        {
            var newStatus = booking.Status;

            if (booking.Status == BookingStatus.Confirmed && booking.CheckinDate <= now && booking.CheckOutDate >= now)
                newStatus = BookingStatus.CheckedIn;
            else if (booking.CheckOutDate < now)
                newStatus = BookingStatus.CheckedOut;

            if (newStatus != booking.Status)
            {
                booking.Status = newStatus;
                await _bookingRepository.UpdateAsync(booking);
            }
        }
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
        var bookings = await _bookingRepository.GetRoomOccupancyByRoomIdAsync(roomId);
        return bookings.Select(b => _mapper.ToDto(b));
    }
    public async Task<IEnumerable<BookingResponseDto>> GetActiveUserBookingsAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetActiveByUserIdAsync(userId);
        return bookings.Select(b => ToDto(b, b.Room.Name,null));
    }

    public async Task<IEnumerable<BookingResponseDto>> GetPreviousUserBookingsAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetPreviousByUserIdAsync(userId);
        return bookings.Select(b => ToDto(b, b.Room.Name,null));
    }

    private static BookingResponseDto ToDto(Booking b, string roomName,string email) => new(
        b.Id,
        b.UserId,
        b.RoomId,
        roomName,
        b.CheckinDate,
        b.CheckOutDate,
        (int)(b.CheckOutDate - b.CheckinDate).TotalDays,
        b.TotalPrice,
        b.Status,
        b.CreatedAt,
        b.Payment?.PaymentMethod,
        email 
    );
}
