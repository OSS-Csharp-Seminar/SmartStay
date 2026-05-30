namespace SmartStay.Application.Dto;

public record ReviewResponseDto(
    Guid Id,
    Guid UserId,
    string UserName,
    int Rating,
    string Comment,
    DateTimeOffset DateCreated
    );