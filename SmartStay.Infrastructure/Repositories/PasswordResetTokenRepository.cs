using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly SmartStayDbContext _db;
    public PasswordResetTokenRepository(SmartStayDbContext db) => _db = db;

    public async Task AddAsync(PasswordResetToken token)
    {
        await _db.PasswordResetTokens.AddAsync(token);
        await _db.SaveChangesAsync();
    }

    public async Task<PasswordResetToken?> GetByUserIdAsync(Guid userId)
        => await _db.PasswordResetTokens.FirstOrDefaultAsync(t => t.UserId == userId);

    public async Task DeleteAsync(PasswordResetToken token)
    {
        _db.PasswordResetTokens.Remove(token);
        await _db.SaveChangesAsync();
    }
}
