using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
}