using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(SmartStayDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Review>> GetAllByRoomIdAsync(Guid roomId)
    {
        return _dbSet.Where(r => r.RoomId == roomId).Include(r => r.User).ToList();
    }
    public async Task<double> GetAverageRatingByRoomIdAsync(Guid roomId)
    {
        var reviews = await _dbSet
            .Where(r => r.RoomId == roomId)
            .ToListAsync();

        if (!reviews.Any()) return 0;

        return reviews.Average(r => r.Rating);
    }

    public async Task<Review?> GetByUserAndRoomAsync(Guid userId, Guid roomId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(r => r.UserId == userId && r.RoomId == roomId);
    }
}