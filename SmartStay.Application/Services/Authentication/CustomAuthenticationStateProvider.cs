using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using SmartStay.Application.Interfaces;

namespace SmartStay.Application.Services.Authentication;

public class CustomAuthenticationStateProvider: AuthenticationStateProvider,ICustomAuthenticationStateProvider
{
    
   // private readonly ILocalStorageService _localStorage; 
   private readonly JwtSecurityTokenHandler _tokenHandler;
   private readonly IHttpContextAccessor _httpContextAccessor;
   private readonly IJSRuntime _js;

   public CustomAuthenticationStateProvider(/*ILocalStorageService localStorage,*/ JwtSecurityTokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor, IJSRuntime js) 
   {
        // _localStorage = localStorage;     
        _js = js;
        _tokenHandler = tokenHandler;
        _httpContextAccessor = httpContextAccessor;
   }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token=null;
        try
        {
            token = await _js.InvokeAsync<string>("getCookie","token"); 
        }
        catch{}

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
                await _js.InvokeVoidAsync("deleteCookie", "token");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            
            //M.G: maps user role to the claim 
            var claims= jwtToken.Claims.Select(c => c.Type == "role"
            ? new Claim(ClaimTypes.Role, c.Value)
            :c).ToList();

            var identity = new ClaimsIdentity(claims, "jwt",ClaimTypes.Name, ClaimTypes.Role);
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
        await _js.InvokeVoidAsync("setCookie", "token", token, 120);
       NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("deleteCookie","token");
        await _js.InvokeVoidAsync("deleteCookie", "id");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

}