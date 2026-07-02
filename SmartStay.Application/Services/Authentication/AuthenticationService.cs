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
    private readonly IMapper<User, UserCreationRequestDto> _userCreationMapper;
    private readonly JwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(IUserRepository userRepository, IPasswordHasher<string> passwordHasher, IMapper<User, UserCreationRequestDto> userCreationMapper, JwtService jwtService, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userCreationMapper=userCreationMapper;
        _jwtService=jwtService;
        _httpContextAccessor=httpContextAccessor;
    }

    public async Task<AuthenticationResponseDto> Authenticate(UserLoginRequestDto dto)
    {
        User user = await _userRepository.GetUserByEmailAsync(dto.Email);


        if (user==null || !_passwordHasher.Validate(dto.Password, user.PasswordHash))
        {
            throw new AuthenticationException("email or password is incorrect"); 
        }

        var token = _jwtService.GenerateToken(user);

        
        return new AuthenticationResponseDto(token,user.Id);
    }

    public async Task<AuthenticationResponseDto> CreateUser(UserCreationRequestDto dto)
    {
        var user = _userCreationMapper.ToSource(dto);

        user.PasswordHash=_passwordHasher.Hash(user.PasswordHash);

        user=await _userRepository.AddAsync(user);

        var token = _jwtService.GenerateToken(user);

        return new AuthenticationResponseDto(token,user.Id);
    }

    //M.G:doesnt delete user but disables account.
    public async Task DeleteAccountAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new KeyNotFoundException("User not found.");
        
        user.FirstName= "Deleted user";
        user.LastName= " ";
        user.Email= $"deleted-user-{user.Id}";
        
        await _userRepository.UpdateAsync(user);
    }

}