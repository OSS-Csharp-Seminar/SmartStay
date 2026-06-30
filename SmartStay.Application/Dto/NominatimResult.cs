
using System.Text.Json.Serialization;

namespace SmartStay.Application.Dto;

public record NominatimResult(
    [property: JsonPropertyName("lat")]
    string Latitude,
    [property: JsonPropertyName("lon")]
    string Longitude
    );