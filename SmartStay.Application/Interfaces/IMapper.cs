using SmartStay.Application.Dto.UserDto;

namespace SmartStay.Application.Interfaces;

public interface IMapper<TSource,TDestination>
{
    UserLoginResponseDto ToDto(TSource source);
}