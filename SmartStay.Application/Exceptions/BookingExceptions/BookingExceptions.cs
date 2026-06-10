namespace SmartStay.Application.Exceptions.BookingExceptions;

public class BookingNotFoundException(Guid id)
    : Exception($"Booking with id '{id}' was not found.");

public class RoomNotAvailableException(DateTimeOffset checkIn, DateTimeOffset checkOut)
    : Exception($"Room is not available from {checkIn:d} to {checkOut:d}.");

public class InvalidBookingDatesException(string message)
    : Exception(message);

public class BookingCancellationException(string message)
    : Exception(message);
