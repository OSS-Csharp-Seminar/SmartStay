using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Dto;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(SmartStayDbContext dbContext) : base(dbContext) {}

    public async Task<IEnumerable<Room>> GetAllRoomsByQueryAsync(RoomQueryDto dto)
    {
        var query = _dbSet.AsQueryable();
        
        query=QueryHelperMethod(query, dto);

        return await query.ToListAsync();
    }
    
    private IQueryable<Room> QueryHelperMethod(IQueryable<Room> query,RoomQueryDto dto)
    {
            query = query.Where(r => r.PricePerNight >= dto.PriceLowerRange);

            query = query.Where(r => r.PricePerNight <= dto.PriceUpperRange);

            query = query.Where(r => r.Size>= dto.GuestNumber);

            //M.G: also shows rooms that have 0 reviews.
            query = query.Where(r => !r.Reviews.Any()
                                     || r.Reviews.Average(rev => rev .Rating) >= dto.Rating);

            query = query.Where(r => dto.Amenities.All(a => r.RoomAmenities.Any(ra => ra.Amenity.Name.Equals(a))));

            //M.G: room availability for given date
            query = query.Where(r => !r.Bookings.Any(b => (b.Status == BookingStatus.CheckedIn
                                                         || b.Status == BookingStatus.Confirmed)
             && b.CheckinDate < dto.FreeTo && b.CheckOutDate > dto.FreeFrom));

            query = dto.SortBy.ToLower() switch
            {
                "price" => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.PricePerNight)
                    : query.OrderBy(r => r.PricePerNight),
                
                "rating" => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.Reviews.Average(rev => rev.Rating))
                    : query.OrderBy(r => r.Reviews.Average(rev => rev.Rating)),
                    
                    _ => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.Reviews.Average(rev => rev.Rating))
                    : query.OrderBy(r => r.Reviews.Average(rev => rev.Rating))
            };
            
            query = query.Skip((int)((dto.Page-1) * dto.PageSize)).Take((int)dto.PageSize);
            
            
        return query;
    }
}
