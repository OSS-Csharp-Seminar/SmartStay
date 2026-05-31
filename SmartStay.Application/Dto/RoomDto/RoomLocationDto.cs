namespace SmartStay.Application.Dto;

public record RoomLocationDto(
    string Country,
    string City,
    string Address,
    double Longitude,
    double Latitude
    );