using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    Task<bool> HasOverlapAsync(Guid roomId, DateTimeOffset checkIn, DateTimeOffset checkOut, Guid? excludeBookingId = null);
    Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Booking>> GetByRenterIdAsync(Guid renterId);
    Task<IEnumerable<Booking>> GetAllActiveForSyncAsync();
    Task<Booking?> GetWithDetailsAsync(Guid id);
    Task CancelAsync(Booking booking, CancellationLog log);
    Task<IEnumerable<Booking>> GetRoomOccupancyByRoomIdAsync(Guid roomId);
    Task<IEnumerable<Booking>> GetActiveByUserIdAsync(Guid userId);
    Task<IEnumerable<Booking>> GetPreviousByUserIdAsync(Guid userId);
}
