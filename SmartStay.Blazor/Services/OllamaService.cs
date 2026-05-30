using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SmartStay.Blazor.Services;

public class OllamaService(IHttpClientFactory factory)
{
    private const string Model = "llama3.2";
    private const string ApiUrl = "http://localhost:11434/api/chat";

    private const string SystemPrompt = """
        You are a helpful assistant embedded inside SmartStay — a hotel management and booking system.

        The system has the following domain:
        - Rooms: name, description, capacity, price per night, size, bed type (Single/Double/Queen/King), average rating, amenities
        - Bookings: check-in/check-out dates, status (Pending/Confirmed/Cancelled/Completed)
        - Users: guests and staff, roles (Guest/Admin), JWT-based authentication
        - Payments: amount, method (CreditCard/Cash/BankTransfer), status (Pending/Completed/Refunded)
        - Reviews: text reviews linked to rooms and users
        - Amenities: features attached to rooms (e.g. WiFi, AC, Pool)
        - CancellationLog: tracks cancellation reasons and timestamps

        Help developers and users understand: how the system works, how entities relate, typical workflows
        (e.g. booking flow, payment flow), and answer questions about features being built.
        Be concise. If something is outside SmartStay scope, say so briefly.
        """;

    public async Task<string> ChatAsync(List<OllamaMessage> history)
    {
        var http = factory.CreateClient();

        var messages = new List<object>
        {
            new { role = "system", content = SystemPrompt }
        };
        messages.AddRange(history.Select(m => (object)new { role = m.Role, content = m.Content }));

        var response = await http.PostAsJsonAsync(ApiUrl, new
        {
            model = Model,
            messages,
            stream = false
        });

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<OllamaResponse>();
        return result?.Message?.Content ?? "(no response)";
    }
}

public record OllamaMessage(string Role, string Content);

public class OllamaResponse
{
    [JsonPropertyName("message")]
    public OllamaMessageBody? Message { get; set; }
}

public class OllamaMessageBody
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
