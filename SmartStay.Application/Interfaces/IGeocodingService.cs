using SmartStay.Application.Dto;

namespace SmartStay.Application.Interfaces;

public interface IGeocodingService
{
    Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address);
 
}