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

        return await query.Include(r => r.Location)
            .Include(r => r.RoomAmenities).ThenInclude(ra => ra.Amenity)
            .Include(r => r.Reviews)
            .ToListAsync();
    }
    
    private IQueryable<Room> QueryHelperMethod(IQueryable<Room> query,RoomQueryDto dto)
    { 
        if (dto.Name!=null)
        {
            query = query.Where(r => r.Name.ToLower()==dto.Name.ToLower());
        }
        
        if( dto.PriceLowerRange != null){
            query = query.Where(r => r.PricePerNight >= dto.PriceLowerRange);
            }

        if (dto.PriceUpperRange != null)
        {
            query = query.Where(r => r.PricePerNight <= dto.PriceUpperRange);
        }

        if (dto.GuestNumber != null)
        {
            query = query.Where(r => r.Capacity>= (int)dto.GuestNumber);
        }

        if (dto.City != null)
        {
           query = query.Where(r => r.Location.City.ToLower()==dto.City.ToLower()); 
        }

        if(dto.Rating!=null){
        //M.G: also shows rooms that have 0 reviews.
            query = query.Where(r => !r.Reviews.Any()
                                     || r.Reviews.Average(rev => rev .Rating) >= dto.Rating);
            }

        if (dto.Amenities != null)
        {
            query = query.Where(r => dto.Amenities.All(a =>
                r.RoomAmenities.Any(ra => ra.Amenity.Name.Equals(a, StringComparison.OrdinalIgnoreCase))));
        }

        if (dto.FreeFrom != null && dto.FreeTo != null)
        {
            //M.G: room availability for given date
            query = query.Where(r => !r.Bookings.Any(b => (b.Status == BookingStatus.CheckedIn
                                                           || b.Status == BookingStatus.Confirmed)
                                                          && b.CheckinDate < dto.FreeTo &&
                                                          b.CheckOutDate > dto.FreeFrom));
        }

        query = dto.SortBy.ToLower() switch
            {
                "name" => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.Name)
                        : query.OrderBy(r => r.Name),
                
                "price" => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.PricePerNight)
                    : query.OrderBy(r => r.PricePerNight),
                
                "rating" => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.Reviews.Average(rev => rev.Rating))
                    : query.OrderBy(r => r.Reviews.Average(rev => rev.Rating)),
                
                "date" => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.CreatedAt)
                    : query.OrderBy(r => r.CreatedAt),
                    
                    _ => (dto.IsDescending)
                    ? query.OrderByDescending(r => r.Reviews.Average(rev => rev.Rating))
                    : query.OrderBy(r => r.Reviews.Average(rev => rev.Rating))
            };
            
            query = query.Skip((int)((dto.Page-1) * dto.PageSize)).Take((int)dto.PageSize);
            
            
        return query;
    }
}
