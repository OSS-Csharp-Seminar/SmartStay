using SmartStay.Application.Dto.UserDto;
using SmartStay.Application.Exceptions;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<string> _passwordHasher;
    private readonly IMapper<User, UserLoginResponseDto> _userMapper;

    public AuthenticationService(IUserRepository userRepository, IPasswordHasher<string> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserLoginResponseDto> Authenticate(UserLoginRequestDto dto)
    {
        User user= await _userRepository.GetUserByEmailAsync(dto.Email);

        if (_passwordHasher.Validate(dto.Password, user.PasswordHash))
        {
            return _userMapper.ToDto(user);
        }
        else
        {
           throw new AuthenticationException("Email or password is incorrect"); 
        }
    }
}