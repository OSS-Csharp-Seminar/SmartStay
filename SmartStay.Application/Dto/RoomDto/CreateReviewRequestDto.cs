public record CreateReviewRequestDto(
    Guid UserId,
    Guid RoomId,
    int Rating,
    string Comment
);