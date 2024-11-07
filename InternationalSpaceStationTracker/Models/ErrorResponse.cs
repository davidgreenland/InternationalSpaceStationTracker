using System.Text.Json.Serialization;

namespace InternationalSpaceStationTracker.Models;

public class ErrorReponse
{
    [JsonPropertyName("error")]
    public string? Message { get; set; }
    [JsonPropertyName("status")]
    public int Status { get; set; }

    public override string ToString()
    {
        return $"{Status} Error: {Message}";
    }
}