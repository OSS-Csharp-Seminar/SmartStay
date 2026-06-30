using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces.Repository;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class RoomImageRepository : Repository<RoomImage>, IRoomImageRepository
{
    public RoomImageRepository(SmartStayDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<RoomImage>> AddMultipleAsync(List<RoomImage> images)
    {
        await _dbSet.AddRangeAsync(images);
        await _dbContext.SaveChangesAsync();
        
        return images;
    }
}