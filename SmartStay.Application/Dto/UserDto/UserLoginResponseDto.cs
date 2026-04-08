namespace SmartStay.Application.Dto.UserDto;

public record UserLoginResponseDto(
    // string Token, M.G: jwt not implemented jet
    Guid UserId
    );