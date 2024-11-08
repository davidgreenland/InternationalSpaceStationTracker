using System.Text.Json.Serialization;

namespace InternationalSpaceStationTracker.Models;

public class Satellite
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public bool IsIss()
    {
        return Name == "iss";
    }

    public override string ToString()
    {
        return $"Name: {Name}, ID: {Id}";
    }
}
