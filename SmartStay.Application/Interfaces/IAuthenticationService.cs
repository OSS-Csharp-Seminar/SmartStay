using SmartStay.Application.Dto.UserDto;

namespace SmartStay.Application;

public interface IAuthenticationService
{
  Task<AuthenticationResponseDto> Authenticate(UserLoginRequestDto dto);  
  Task<AuthenticationResponseDto> CreateUser(UserCreationRequestDto dto);  
}