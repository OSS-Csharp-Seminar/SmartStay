using SmartStay.Application.Dto.UserDto;

namespace SmartStay.Application.Interfaces;

public interface IMapper<TSource,TDestination>
{
    TDestination ToDto(TSource source);
    
    TSource ToSource(TDestination destination);
}