namespace SmartStay.Domain.Dto;

public record RoomQueryDto(   
    uint Page,
    uint PageSize,
   
    uint PriceLowerRange,
    uint PriceUpperRange,
    uint GuestNumber,
    string Amenities,
    float Rating,
    DateTimeOffset FreeFrom,
    DateTimeOffset FreeTo,
   
    string SortBy,
    bool IsDescending
    );