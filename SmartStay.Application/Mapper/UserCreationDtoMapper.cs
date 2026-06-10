using SmartStay.Application.Dto.UserDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Mapper;

public class UserCreationDtoMapper : IMapper<User,UserCreationRequestDto>
{
    public UserCreationRequestDto ToDto(User source)
    {
        throw new NotImplementedException();
    }

    public User ToSource(UserCreationRequestDto destination)
    {
        return new User{
           Email = destination.Email,
           PasswordHash = destination.Password,
           FirstName = destination.FirstName,
           LastName = destination.LastName,
           // Role = destination.Role,
        };
    }
}