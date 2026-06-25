using SmartStay.Application.Dto.RecommendationDto;

namespace SmartStay.Application.Interfaces;

public interface IRecommendationService
{
    Task<List<RoomRecommendationDto>> GetRecommendationsAsync(RecommendationRequestDto request);
    Task<List<string>> GetCitiesAsync();
}
