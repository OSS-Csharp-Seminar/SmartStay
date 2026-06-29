using SmartStay.Domain.Enums;

namespace SmartStay.Application.Dto.UserDto;

public record UserCreationRequestDto(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    Role Role
    );