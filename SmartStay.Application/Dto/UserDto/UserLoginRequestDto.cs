namespace SmartStay.Application.Dto.UserDto;

public record UserLoginRequestDto(
    string Email,
    string Password
    );