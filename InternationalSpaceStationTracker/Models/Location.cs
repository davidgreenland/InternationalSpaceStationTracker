using System.Globalization;
using System.Text.Json.Serialization;

namespace InternationalSpaceStationTracker.Models;

public class Location
{
    [JsonPropertyName("latitude")]
    public string Latitude { get; set; }
    [JsonPropertyName("longitude")]
    public string Longitude { get; set; }
    [JsonPropertyName("timezone_id")]
    public string TimezoneId { get; set; }
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }
    [JsonPropertyName("map_url")]
    public string? MapUrl { get; set; }

    public override string ToString()
    {
        string country;
        if (CountryCode != null)
        {
            var cultureInfo = new CultureInfo(CountryCode);
            var ri = new RegionInfo(cultureInfo.Name);
            country = ri.EnglishName;
        } 
        else
        {
            country = "Not over a country";
        }

        return $"TimeZone: {TimezoneId}, Country: {country}";
    }
}

