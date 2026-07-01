using SmartStay.Domain.Enums;

namespace SmartStay.Application.Dto.RoomDto;

public record RoomUpdateDto(
    string Name,
    string? Description,
    int Capacity,
    float PricePerNight,
    int Size,
    BedType BedType
);
