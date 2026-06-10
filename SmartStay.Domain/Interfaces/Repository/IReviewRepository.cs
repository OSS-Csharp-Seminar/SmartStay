using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces;

public interface IReviewRepository:IRepository<Review>
{
    Task<IEnumerable<Review>> GetAllByRoomIdAsync(Guid roomId);

}