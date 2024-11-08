using System.Text.Json.Serialization;

namespace InternationalSpaceStationTracker.Models
{
    public class SatelliteDetail : Satellite
    {
        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }
        [JsonPropertyName("altitude")]
        public decimal Altitude { get; set; }
        [JsonPropertyName("velocity")]
        public decimal Velocity { get; set; }
        [JsonPropertyName("visibility")]
        public string? Visibility { get; set; }
        [JsonPropertyName("footprint")]
        public decimal Footprint { get; set; }
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        [JsonPropertyName("daynum")]
        public decimal DayNum { get; set; }
        [JsonPropertyName("solar_lat")]
        public decimal SolarLat { get; set; }
        [JsonPropertyName("solar_lon")]
        public decimal SolarLon { get; set; }
        [JsonPropertyName("units")]
        public string? Units { get; set; }

        public override string ToString() => $"{base.ToString()}\nLatitude: {Latitude}\nLongitude: {Longitude}\nVelocity: {Velocity:F2} {(Units == "miles" ? "mph" : "kph")}";
    }
}
