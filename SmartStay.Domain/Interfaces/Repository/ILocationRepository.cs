using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces.Repository;

public interface ILocationRepository : IRepository<Location>
{
   Task<Location> GetLocationByAddressAsync(string address); 
}