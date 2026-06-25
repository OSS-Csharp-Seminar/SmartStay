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
            You are a helpful assistant embedded inside SmartStay — a hotel management and booking system.

            Domain overview:
            - Rooms: name, description, capacity, price per night, size, bed type (Single/Double/Queen/King), average rating, amenities, location (city)
            - Bookings: check-in/check-out dates, status (Pending/Confirmed/CheckedIn/CheckedOut/Cancelled/Completed)
            - Users: guests and staff, roles (Guest/Admin), JWT-based authentication
            - Payments: amount, method (CreditCard/Cash/BankTransfer), status (Pending/Completed/Refunded)
            - Reviews: text reviews linked to rooms and users
            - Amenities: features attached to rooms (e.g. WiFi, AC, Pool)

            ROOMS CURRENTLY IN THE SYSTEM:
            {roomContext}

            When users ask about room recommendations, use the room data above to suggest specific options.
            You can only READ data — never suggest or imply any modification to the database.
            Be concise and helpful.
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
