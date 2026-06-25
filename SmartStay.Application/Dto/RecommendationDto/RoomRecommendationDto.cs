using SmartStay.Domain.Enums;

namespace SmartStay.Application.Dto.RecommendationDto;

public record RoomRecommendationDto(
    Guid Id,
    string Name,
    string? Description,
    float PricePerNight,
    int Capacity,
    BedType BedType,
    float AverageRating,
    string? City,
    List<string> Amenities,
    int MatchScore
);
