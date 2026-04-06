using SmartStay.Application.Dto.UserDto;

namespace SmartStay.Application;

public interface IAuthenticationService
{
  Task<UserLoginResponseDto> Authenticate(UserLoginRequestDto dto);  
}