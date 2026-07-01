using SmartStay.Application.Dto;

namespace SmartStay.Application.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewResponseDto>> GetAllByRoomIdAsync(Guid roomId);
    Task<ReviewResponseDto> CreateReviewAsync(CreateReviewRequestDto dto);  
    Task<ReviewResponseDto> UpdateReviewAsync(Guid id, UpdateReviewRequestDto dto); 
    Task DeleteReviewAsync(Guid id); // ← novo
}