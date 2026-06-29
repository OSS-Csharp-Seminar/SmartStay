using SmartStay.Application.Interfaces;
using SmartStay.Application.Util;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services.Authentication;

public class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordResetTokenRepository _tokenRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher<string> _passwordHasher;

    public PasswordRecoveryService(
        IUserRepository userRepository,
        IPasswordResetTokenRepository tokenRepository,
        IEmailService emailService,
        IPasswordHasher<string> passwordHasher)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _emailService = emailService;
        _passwordHasher = passwordHasher;
    }

    public async Task SendResetCodeAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return;

        var existing = await _tokenRepository.GetByUserIdAsync(user.Id);
        if (existing != null) await _tokenRepository.DeleteAsync(existing);

        var code = Random.Shared.Next(100000, 999999).ToString();
        var token = new PasswordResetToken
        {
            UserId = user.Id,
            Code = code,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5)
        };

        await _tokenRepository.AddAsync(token);
        await _emailService.SendAsync(
            email,
            "SmartStay - Password Reset Code",
            $"Your password reset code is: {code}\n\nThis code expires in 5 minutes.");
    }

    public async Task<bool> VerifyCodeAsync(string email, string code)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return false;

        var token = await _tokenRepository.GetByUserIdAsync(user.Id);
        if (token == null) return false;
        if (token.ExpiresAt < DateTime.UtcNow) return false;
        if (token.Code != code) return false;

        return true;
    }

    public async Task ResetPasswordAsync(string email, string newPassword)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return;

        user.PasswordHash = _passwordHasher.Hash(newPassword);
        await _userRepository.UpdateAsync(user);

        var token = await _tokenRepository.GetByUserIdAsync(user.Id);
        if (token != null) await _tokenRepository.DeleteAsync(token);
    }
}
