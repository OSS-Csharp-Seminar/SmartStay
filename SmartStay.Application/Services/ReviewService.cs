using SmartStay.Application.Dto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services;

public class ReviewService : IReviewService
{
   private readonly IReviewRepository _reviewRepo;
   private readonly IMapper<Review,ReviewResponseDto> _mapper;

   public ReviewService(IReviewRepository reviewRepo, IMapper<Review, ReviewResponseDto> mapper)
   {
       _reviewRepo=reviewRepo;
       _mapper=mapper;
   }
    
    public async Task<IEnumerable<ReviewResponseDto>> GetAllByRoomIdAsync(Guid roomId)
    {
        var reviews =await _reviewRepo.GetAllByRoomIdAsync(roomId);

        return reviews.Select(r => _mapper.ToDto(r)).ToList();
    }

}