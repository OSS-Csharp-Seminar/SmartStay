using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces.Repository;

public interface IRoomImageRepository : IRepository<RoomImage>
{
   Task<IEnumerable<RoomImage>> AddMultipleAsync(List<RoomImage> images); 
}