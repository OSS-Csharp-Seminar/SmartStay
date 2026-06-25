using SmartStay.Application.Dto.RecommendationDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Dto;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services.Recommendation;

public class RecommendationService(IRoomRepository roomRepository) : IRecommendationService
{
    private const float WeightPrice      = 0.35f;
    private const float WeightAmenities  = 0.25f;
    private const float WeightCapacity   = 0.20f;
    private const float WeightRating     = 0.10f;
    private const float WeightPopularity = 0.10f;

    private static readonly RoomQueryDto AllRoomsQuery =
        new(null, null, null, null, null, null, null, null, null, null, null, null, false);

    public async Task<List<string>> GetCitiesAsync()
    {
        var rooms = await roomRepository.GetAllRoomsByQueryAsync(AllRoomsQuery);
        return rooms
            .Where(r => r.Location?.City != null)
            .Select(r => r.Location!.City)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }

    public async Task<List<RoomRecommendationDto>> GetRecommendationsAsync(RecommendationRequestDto request)
    {
        var rooms = (await roomRepository.GetAllRoomsByQueryAsync(AllRoomsQuery)).ToList();

        // City is a hard filter — only keep rooms in the requested city
        if (!string.IsNullOrWhiteSpace(request.City))
            rooms = rooms
                .Where(r => string.Equals(r.Location?.City, request.City, StringComparison.OrdinalIgnoreCase))
                .ToList();

        // Filter out rooms that can't fit the guests
        var eligible = rooms.Where(r => r.Capacity >= request.GuestCount).ToList();

        var maxBookings = eligible.Any() ? eligible.Max(r => r.Bookings?.Count ?? 0) : 1;
        if (maxBookings == 0) maxBookings = 1;

        return eligible
            .Select(room => new RoomRecommendationDto(
                Id:            room.Id,
                Name:          room.Name,
                Description:   room.Description,
                PricePerNight: room.PricePerNight,
                Capacity:      room.Capacity,
                BedType:       room.BedType,
                AverageRating: room.AverageRating,
                City:          room.Location?.City,
                Amenities:     room.RoomAmenities?.Select(ra => ra.Amenity.Name).ToList() ?? [],
                MatchScore:    CalculateScore(room, request, maxBookings)
            ))
            .OrderByDescending(r => r.MatchScore)
            .ToList();
    }

    private static int CalculateScore(Room room, RecommendationRequestDto request, int maxBookings)
    {
        var score = 0f;

        // Price Match (35%)
        if (request.MaxBudget == null || room.PricePerNight <= request.MaxBudget)
        {
            score += WeightPrice;
        }
        else
        {
            var overRatio = (room.PricePerNight - request.MaxBudget.Value) / request.MaxBudget.Value;
            score += WeightPrice * Math.Max(0f, 1f - overRatio);
        }

        // Amenities Match (25%)
        if (request.WantedAmenities == null || request.WantedAmenities.Count == 0)
        {
            score += WeightAmenities;
        }
        else
        {
            var roomAmenities = room.RoomAmenities?
                .Select(ra => ra.Amenity.Name.ToLower())
                .ToHashSet() ?? [];

            var matched = request.WantedAmenities.Count(a => roomAmenities.Contains(a.ToLower()));
            score += WeightAmenities * ((float)matched / request.WantedAmenities.Count);
        }

        // Capacity Match (20%) — room already passed the capacity >= guestCount filter,
        // so it can always fit the guests. No penalty for extra space.
        score += WeightCapacity;

        // Rating (10%)
        score += WeightRating * (room.AverageRating / 5f);

        // Popularity (10%)
        var bookingCount = room.Bookings?.Count ?? 0;
        score += WeightPopularity * ((float)bookingCount / maxBookings);

        // Bed type bonus — only if preference set AND room matches.
        // Non-matching rooms are NOT penalized; no preference = no bonus for anyone.
        if (request.PreferredBedType != null && room.BedType == request.PreferredBedType)
            score = Math.Min(1f, score + 0.05f);

        return (int)Math.Round(score * 100);
    }
}
