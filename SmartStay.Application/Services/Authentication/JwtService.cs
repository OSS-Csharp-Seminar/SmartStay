using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Util;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("a9F3kLm2Xq7ZpR8vT1nW6cB4yH0uD5Js")); 
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer:"SmartStay",
            audience: "SmartStay",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    } 
}