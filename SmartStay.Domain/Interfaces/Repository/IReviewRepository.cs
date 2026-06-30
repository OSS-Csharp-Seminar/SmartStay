using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces;

public interface IReviewRepository:IRepository<Review>
{
    Task<IEnumerable<Review>> GetAllByRoomIdAsync(Guid roomId);
    Task<double> GetAverageRatingByRoomIdAsync(Guid roomId); 
    Task<Review?> GetByUserAndRoomAsync(Guid userId, Guid roomId); 
}