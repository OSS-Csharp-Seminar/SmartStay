namespace SmartStay.Application.Dto.UserDto;

public record AuthenticationResponseDto(
    string Token,
    Guid UserId
    );