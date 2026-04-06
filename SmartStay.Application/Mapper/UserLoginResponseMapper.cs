using SmartStay.Application.Dto.UserDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Mapper;

public class UserLoginResponseMapper : IMapper<User,UserLoginResponseDto>
{
   public UserLoginResponseDto ToDto(User user)
   {
      return new UserLoginResponseDto(user.Id);
   }
}