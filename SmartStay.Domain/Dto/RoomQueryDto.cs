namespace SmartStay.Domain.Dto;

public record RoomQueryDto(   
    uint? Page,
    uint? PageSize,
   
    string? Name,
    float? PriceLowerRange,
    float? PriceUpperRange,
    uint? GuestNumber,
    List<string>? Amenities,
    float? Rating,
    DateTimeOffset? FreeFrom,
    DateTimeOffset? FreeTo,
    string? City,
   
    string? SortBy,
    bool IsDescending
    );