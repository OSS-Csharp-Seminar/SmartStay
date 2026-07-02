using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;
using System.Data;

namespace SmartStay.Infrastructure.Repositories;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(SmartStayDbContext dbContext) : base(dbContext) { }

    public async Task<bool> HasOverlapAsync(Guid roomId, DateTimeOffset checkIn, DateTimeOffset checkOut, Guid? excludeBookingId = null)
    {
        return await _dbContext.Bookings
            .Where(b =>
                b.RoomId == roomId &&
                b.Status != BookingStatus.Cancelled &&
                (excludeBookingId == null || b.Id != excludeBookingId) &&
                b.CheckinDate < checkOut &&
                b.CheckOutDate > checkIn)
            .AnyAsync();
    }

    public async Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.Bookings
            .Include(b => b.Room).ThenInclude(r => r.Renter)
            .Include(b => b.Payment)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetByRenterIdAsync(Guid renterId)
    {
        return await _dbContext.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Include(b => b.Payment)
            .Where(b => b.Room.RenterId == renterId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetAllActiveForSyncAsync()
    {
        return await _dbContext.Bookings
            .Where(b => b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetRoomOccupancyByRoomIdAsync(Guid roomId)
    {
       return await _dbSet.Where(b => b.RoomId == roomId 
                                      && ( b.Status==BookingStatus.Confirmed
                                      || b.Status==BookingStatus.CheckedIn
                                      )).ToListAsync(); 
    }

    public async Task<Booking?> GetWithDetailsAsync(Guid id)
    {
        return await _dbContext.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Include(b => b.Payment)
            .Include(b => b.CancellationLog)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
    public async Task<IEnumerable<Booking>> GetActiveByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(b => b.Room)
            .Where(b => b.UserId == userId &&
                        (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn))
            .OrderBy(b => b.CheckinDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetPreviousByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(b => b.Room)
            .Where(b => b.UserId == userId && b.Status == BookingStatus.CheckedOut)
            .OrderByDescending(b => b.CheckOutDate)
            .ToListAsync();
    }

    public async Task<bool> CanUserLeaveReviewAsync(Guid userId, Guid RoomId)
    {
       return await _dbSet.AnyAsync(
           b => b.RoomId == RoomId
                && b.UserId == userId
                && b.Status == BookingStatus.CheckedOut); 
    }

    // Cancellation updates booking status and creates the log atomically in one transaction
    public async Task CancelAsync(Booking booking, CancellationLog log)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        booking.Status = BookingStatus.Cancelled;
        _dbContext.Bookings.Update(booking);
        await _dbContext.CancellationLogs.AddAsync(log);
        await _dbContext.SaveChangesAsync();

        await transaction.CommitAsync();
    }
}
