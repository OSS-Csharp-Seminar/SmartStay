using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces;

public interface IPasswordResetTokenRepository
{
    Task AddAsync(PasswordResetToken token);
    Task<PasswordResetToken?> GetByUserIdAsync(Guid userId);
    Task DeleteAsync(PasswordResetToken token);
}
