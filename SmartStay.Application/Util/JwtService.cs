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
        Debug.WriteLine("Key: " + _configuration["Jwt:Key"]);
        Debug.WriteLine("Issuer: " + _configuration["Jwt:Issuer"]);
        Debug.WriteLine("Audience: " + _configuration["Jwt:Audience"]);
        Debug.WriteLine("User: " + user?.Email); 
        
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)); //M.G: breaking point
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    } 
}