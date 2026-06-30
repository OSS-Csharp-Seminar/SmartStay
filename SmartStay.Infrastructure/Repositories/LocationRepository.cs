using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces.Repository;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class LocationRepository : Repository<Location>, ILocationRepository
{
    public LocationRepository(SmartStayDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Location> GetLocationByAddressAsync(string address)
    {
       return await _dbSet.FirstOrDefaultAsync(l => l.Address == address); 
    }
}