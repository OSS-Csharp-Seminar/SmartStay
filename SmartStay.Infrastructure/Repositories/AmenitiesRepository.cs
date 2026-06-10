using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class AmenitiesRepository : Repository<Amenity>, IAmenitiesRepository
{
    public AmenitiesRepository(SmartStayDbContext dbContext) : base(dbContext)
    {
    }
}