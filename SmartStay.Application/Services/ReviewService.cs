using SmartStay.Application.Dto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepo;
    private readonly IRoomRepository _roomRepo;
    private readonly IMapper<Review, ReviewResponseDto> _mapper;

    public ReviewService(
        IReviewRepository reviewRepo,
        IRoomRepository roomRepo,
        IMapper<Review, ReviewResponseDto> mapper)
    {
        _reviewRepo = reviewRepo;
        _roomRepo = roomRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewResponseDto>> GetAllByRoomIdAsync(Guid roomId)
    {
        var reviews = await _reviewRepo.GetAllByRoomIdAsync(roomId);
        return reviews.Select(r => _mapper.ToDto(r)).ToList();
    }

    public async Task<ReviewResponseDto> CreateReviewAsync(CreateReviewRequestDto dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        var existing = await _reviewRepo.GetByUserAndRoomAsync(dto.UserId, dto.RoomId);
        if (existing != null)
            throw new InvalidOperationException("User already reviewed this room.");

        var review = new Review
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CreatedAt = DateTimeOffset.UtcNow
        };


        await _reviewRepo.AddAsync(review);

        await RecalculateAverageRatingAsync(dto.RoomId);

        return _mapper.ToDto(review);
    }

    public async Task<ReviewResponseDto> UpdateReviewAsync(Guid id, UpdateReviewRequestDto dto)
    {
        var review = await _reviewRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Review '{id}' not found.");

        if (dto.Rating.HasValue)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");
            review.Rating = dto.Rating.Value;
        }

        if (dto.Comment != null)
            review.Comment = dto.Comment;

        await _reviewRepo.UpdateAsync(review);

        await RecalculateAverageRatingAsync(review.RoomId);

        return _mapper.ToDto(review);
    }

    public async Task DeleteReviewAsync(Guid id)
    {
        var review = await _reviewRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Review '{id}' not found.");

        var roomId = review.RoomId;

     
        await _reviewRepo.DeleteAsync(review);

        await RecalculateAverageRatingAsync(roomId);
    }

    private async Task RecalculateAverageRatingAsync(Guid roomId)
    {
        var average = await _reviewRepo.GetAverageRatingByRoomIdAsync(roomId);
        await _roomRepo.UpdateAverageRatingAsync(roomId, (float)average);
    }
}