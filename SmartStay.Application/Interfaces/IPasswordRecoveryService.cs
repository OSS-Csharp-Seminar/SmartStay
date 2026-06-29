namespace SmartStay.Application.Interfaces;

public interface IPasswordRecoveryService
{
    Task SendResetCodeAsync(string email);
    Task<bool> VerifyCodeAsync(string email, string code);
    Task ResetPasswordAsync(string email, string newPassword);
}
