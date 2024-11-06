using System.Globalization;
using System.Text.Json.Serialization;

namespace InternationalSpaceStationTracker.Models;

public class Location
{
    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; }
    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; }
    [JsonPropertyName("timezone_id")]
    public string? TimezoneId { get; set; }
    [JsonPropertyName("offset")]
    public decimal Offset { get; set; }
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }
    [JsonPropertyName("map_url")]
    public string? MapUrl { get; set; }

    public override string ToString()
    {
        return $"TimeZone: {TimezoneId}\n{(IsOcean() ? "Location : Ocean" : $"Country: {GetCountry()}")}\nMapUrl = {MapUrl}";
    }

    private bool IsOcean()
    {
        return CountryCode == "??";
    }

    private string GetCountry()
    {
        if (String.IsNullOrEmpty(CountryCode) || CountryCode == "??")
        {
            return "Country unknown";
        }
        else
        {
            var cultureInfo = new CultureInfo(CountryCode);
            var ri = new RegionInfo(cultureInfo.Name);
            return ri.EnglishName;
        }
    }
}

