namespace SmartStay.Application.Interfaces;

public interface ICustomAuthenticationStateProvider
{
    Task NotifyUserAuthenticationAsync(string token);
    Task LogoutAsync();
}