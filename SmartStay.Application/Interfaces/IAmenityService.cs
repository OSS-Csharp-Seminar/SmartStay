using SmartStay.Application.Dto;

namespace SmartStay.Application.Interfaces;

public interface IAmenityService
{
   Task<IEnumerable<AmenityResponseDto>> GetAmenitiesAsync(); 
}