using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;

namespace SmartStay.Domain.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<Room> GetRoomByNameAsync(string name);
    Task<Room> GetRoomByCapacityAsync(int capacity);
    Task<Room> GetRoomByPrice(float price);
    Task<Room> GetRoomBySize(int size);
    Task<Room> GetRoomByBedType(BedType bedType);
    Task<Room> GetRoomByAverageRating(float averageRating);
}