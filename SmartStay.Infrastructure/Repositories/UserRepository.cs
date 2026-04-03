using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure;

public class UserRepository: Repository<User>, IUserRepository
{
    public UserRepository(SmartStayDbContext dbContext) : base(dbContext)
    {}
    
    public async Task<User?> GetUserByEmailAsync(string email)
    => await _dbSet.FirstOrDefaultAsync(u => u.Email==email);
    
}