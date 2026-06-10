using SmartStay.Application.Dto;

namespace SmartStay.Application.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewResponseDto>> GetAllByRoomIdAsync(Guid roomId);
}