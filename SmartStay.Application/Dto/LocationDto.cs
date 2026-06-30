namespace SmartStay.Application.Dto;

public record LocationDto(
    string City,
    string Country,
    string Address,
    string PostalCode,
    float Latitude,
    float Longitude
    );