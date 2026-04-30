using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using SmartStay.Application.Dto.UserDto;
using SmartStay.Application.Exceptions;
using SmartStay.Application.Interfaces;
using SmartStay.Application.Util;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<string> _passwordHasher;
    private readonly IMapper<User, UserLoginResponseDto> _userMapper;
    private readonly IMapper<User, UserCreationRequestDto> _userCreationMapper;
    private readonly JwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(IUserRepository userRepository, IPasswordHasher<string> passwordHasher, IMapper<User, UserLoginResponseDto> userMapper,IMapper<User, UserCreationRequestDto> userCreationMapper, JwtService jwtService, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userCreationMapper=userCreationMapper;
        _userMapper=userMapper;
        _jwtService=jwtService;
        _httpContextAccessor=httpContextAccessor;
    }

    public async Task<UserLoginResponseDto> Authenticate(UserLoginRequestDto dto)
    {
            User user = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            

        if (!_passwordHasher.Validate(dto.Password, user.PasswordHash))
        {
            throw new AuthenticationException("email or password is incorrect"); 
        }
        
        Debug.WriteLine("User: " + user.Email);

        //M.G:no authentication for now
        // var token = _jwtService.GenerateToken(user);//M.G: throws error: Value cannot be null. (Parameter 's')
        
        // _httpContextAccessor.HttpContext!.Response.Cookies.Append("auth_token", token, new CookieOptions
        // {
        //     HttpOnly = true,
        //     Secure = true,
        //     SameSite = SameSiteMode.Strict,
        //     Expires = DateTimeOffset.UtcNow.AddMinutes(120)
        // });
        
        return _userMapper.ToDto(user);
    }

    public async Task CreateUser(UserCreationRequestDto dto)
    {
        
        
        var user = _userCreationMapper.ToSource(dto);
        
        
        user.PasswordHash=_passwordHasher.Hash(user.PasswordHash);
        
        await _userRepository.AddAsync(user);
    }
}