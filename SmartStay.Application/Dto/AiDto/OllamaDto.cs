using System.Text.Json.Serialization;

namespace SmartStay.Application.Dto.AiDto;

public record OllamaMessageDto(string Role, string Content);

public class OllamaApiResponse
{
    [JsonPropertyName("message")]
    public OllamaApiMessageBody? Message { get; set; }
}

public class OllamaApiMessageBody
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
