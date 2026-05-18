using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SmartStay.Domain.Dto;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(SmartStayDbContext dbContext) : base(dbContext) { }


    public Task<IEnumerable<Room>> GetAllRoomsByQueryAsync(RoomQueryDto dto)
    {
        var query = _dbSet.AsQueryable();
        
        QueryHelperMethod(query, dto);
        
        return 
    }
    
    private IQueryable<Room> QueryHelperMethod(IQueryable<Room> query,RoomQueryDto dto)
    {
            query = query.Where(r => r.PricePerNight >= dto.PriceLowerRange);

            query = query.Where(r => r.PricePerNight <= dto.PriceUpperRange);

            query = query.Where(r => r.Size>= dto.GuestNumber);

            query = query.Where(r => r.Reviews.Any())
                .Where(r => r.Reviews.Average(rev => rev.Rating) >= dto.Rating);
            
            //amenities

           query = query.Where(r => !r.Bookings.Any(b => b.Status == BookingStatus.CheckedIn
                                                         && b.Status == BookingStatus.Confirmed
             && (b.CheckinDate < dto.FreeTo || b.CheckOutDate > dto.FreeFrom)));
           
            
        return query;
    }
}
