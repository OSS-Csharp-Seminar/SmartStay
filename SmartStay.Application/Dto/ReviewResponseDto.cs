namespace SmartStay.Application.Dto;

public record ReviewResponseDto(
    Guid Id,
    Guid UserId,
    string UserName,
    Guid RoomId,  
    int Rating,
    string Comment,
    DateTimeOffset DateCreated
    );