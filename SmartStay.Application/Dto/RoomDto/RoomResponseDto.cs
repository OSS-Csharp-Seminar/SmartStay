using SmartStay.Domain.Enums;

namespace SmartStay.Application.Dto.RoomDto;

public record RoomResponseDto(
    Guid Id,
   string Name,
   string? Description,
    int Capacity,
    float PricePerNight,
    int Size,
    BedType BedType,
    float AverageRating,
    RoomLocationDto Location,
    List<string> Amenities,
    string ImagePath
    );