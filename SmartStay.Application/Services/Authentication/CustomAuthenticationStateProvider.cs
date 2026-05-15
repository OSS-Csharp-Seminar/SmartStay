using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SmartStay.Application.Interfaces;

namespace SmartStay.Application.Services.Authentication;

public class CustomAuthenticationStateProvider: AuthenticationStateProvider,ICustomAuthenticationStateProvider
{
    
   private readonly ILocalStorageService _localStorage; 
   private readonly JwtSecurityTokenHandler _tokenHandler;

   public CustomAuthenticationStateProvider(ILocalStorageService localStorage, JwtSecurityTokenHandler tokenHandler) 
   {
        _localStorage = localStorage;     
        _tokenHandler = tokenHandler;
   }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("token");

        if (string.IsNullOrWhiteSpace(token))
        {   
            //M.G: tells app that user is not logged in. 
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        try
        {
            var jwtToken = _tokenHandler.ReadJwtToken(token);
            

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync("token");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        
    }

    public async Task NotifyUserAuthenticationAsync(string token)
    {
       await _localStorage.SetItemAsync("token", token); 
       NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("token");
        await _localStorage.RemoveItemAsync("id");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

}