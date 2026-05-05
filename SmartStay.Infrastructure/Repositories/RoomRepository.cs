using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(SmartStayDbContext dbContext) : base(dbContext) { }

    public async Task<Room> GetRoomByNameAsync(string name)
        => await _dbSet.FirstOrDefaultAsync(r => r.Name == name)
           ?? throw new KeyNotFoundException($"Room with name '{name}' not found.");

    public async Task<Room> GetRoomByCapacityAsync(int capacity)
        => await _dbSet.FirstOrDefaultAsync(r => r.Capacity == capacity)
           ?? throw new KeyNotFoundException($"Room with capacity '{capacity}' not found.");

    public async Task<Room> GetRoomByPrice(float price)
        => await _dbSet.FirstOrDefaultAsync(r => r.PricePerNight == price)
           ?? throw new KeyNotFoundException($"Room with price '{price}' not found.");

    public async Task<Room> GetRoomBySize(int size)
        => await _dbSet.FirstOrDefaultAsync(r => r.Size == size)
           ?? throw new KeyNotFoundException($"Room with size '{size}' not found.");

    public async Task<Room> GetRoomByBedType(BedType bedType)
        => await _dbSet.FirstOrDefaultAsync(r => r.BedType == bedType)
           ?? throw new KeyNotFoundException($"Room with bed type '{bedType}' not found.");

    public async Task<Room> GetRoomByAverageRating(float averageRating)
        => await _dbSet.FirstOrDefaultAsync(r => r.AverageRating == averageRating)
           ?? throw new KeyNotFoundException($"Room with average rating '{averageRating}' not found.");
}
