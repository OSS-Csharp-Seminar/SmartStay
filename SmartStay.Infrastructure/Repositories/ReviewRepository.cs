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
}