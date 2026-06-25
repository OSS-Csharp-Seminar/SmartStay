using SmartStay.Domain.Enums;

namespace SmartStay.Application.Dto.RecommendationDto;

public record RecommendationRequestDto(
    float? MaxBudget,
    int GuestCount,
    List<string>? WantedAmenities,
    BedType? PreferredBedType,
    string? City
);
