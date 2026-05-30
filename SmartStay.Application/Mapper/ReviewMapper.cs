using SmartStay.Application.Dto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Entities;

namespace SmartStay.Application.Mapper;

public class ReviewMapper : IMapper<Review,ReviewResponseDto>
{
    public ReviewResponseDto ToDto(Review source)
    {
        return new ReviewResponseDto(
            source.Id,
            source.UserId,
            source.User.FirstName+" "+source.User.LastName,
            source.Rating,
            source.Comment,
            source.CreatedAt
        );
    }

    public Review ToSource(ReviewResponseDto destination)
    {
        throw new NotImplementedException();
    }
}