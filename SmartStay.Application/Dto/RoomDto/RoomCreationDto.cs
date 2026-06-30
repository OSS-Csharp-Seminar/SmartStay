using SmartStay.Domain.Entities;
using SmartStay.Domain.Enums;

namespace SmartStay.Application.Dto.RoomDto;

public record RoomCreationDto(
    string Name,
    string Description,
    int Capacity,
    float PricePerNight,
    int Size,
    BedType BedType,
    List<Guid> RoomAmenities,
    List<string> ImagePaths,
    LocationDto Location
    );