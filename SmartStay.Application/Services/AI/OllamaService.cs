using System.Net.Http.Json;
using System.Text;
using SmartStay.Application.Dto.AiDto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Dto;
using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;

namespace SmartStay.Application.Services.AI;

public class OllamaService(HttpClient http, IRoomRepository roomRepository) : IAiService
{
    private const string Model = "llama3.2";
    private const string ApiUrl = "http://localhost:11434/api/chat";

    // Empty query = no filters, no pagination → returns all rooms with Location + Amenities loaded
    private static readonly RoomQueryDto AllRoomsQuery =
        new(null, null, null, null, null, null, null, null, null, null, null, null, false);

    public async Task<string> ChatAsync(List<OllamaMessageDto> history)
    {
        var rooms = await roomRepository.GetAllRoomsByQueryAsync(AllRoomsQuery);
        var roomContext = BuildRoomContext(rooms);

        var systemPrompt = $"""
            You are a helpful assistant inside SmartStay, a hotel booking app.

            Rules you must follow:
            1. Reply in the same language the user writes in. Croatian input = Croatian reply. English input = English reply.
            2. You only know about the rooms listed below. You have NO information about any user's bookings, check-ins, payments or account. Do not guess or make up this information.
            3. If asked about personal bookings (e.g. "which room am I in", "what are my reservations"), reply with exactly: "Za informacije o vašim rezervacijama posjetite stranicu 'My Bookings' u aplikaciji." (or in English: "For your booking information, visit the 'My Bookings' page in the app.")
            4. Keep answers short and natural. No formal or robotic language.
            5. Only answer questions about rooms or general booking questions.

            AVAILABLE ROOMS:
            {roomContext}
            """;

        var messages = new List<object>
        {
            new { role = "system", content = systemPrompt }
        };
        messages.AddRange(history.Select(m => (object)new { role = m.Role, content = m.Content }));

        var response = await http.PostAsJsonAsync(ApiUrl, new
        {
            model = Model,
            messages,
            stream = false
        });

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<OllamaApiResponse>();
        return result?.Message?.Content ?? "(no response)";
    }

    private static string BuildRoomContext(IEnumerable<Room> rooms)
    {
        var sb = new StringBuilder();
        foreach (var room in rooms)
        {
            sb.Append($"- {room.Name}: capacity={room.Capacity}, price=${room.PricePerNight}/night, " +
                      $"size={room.Size}m², bed={room.BedType}, rating={room.AverageRating:F1}");
            if (room.Location is not null)
                sb.Append($", city={room.Location.City}");
            var amenities = room.RoomAmenities?.Select(ra => ra.Amenity?.Name).Where(n => n != null).ToList();
            if (amenities?.Count > 0)
                sb.Append($", amenities=[{string.Join(", ", amenities)}]");
            if (!string.IsNullOrEmpty(room.Description))
                sb.Append($", \"{room.Description}\"");
            sb.AppendLine();
        }
        return sb.Length > 0 ? sb.ToString() : "No rooms available yet.";
    }
}
