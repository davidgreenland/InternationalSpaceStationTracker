using InternationalSpaceStationTracker.Models;
using System.Text.Json;
using System.Web;

namespace InternationalSpaceStationTracker.Services;

public class SatelliteService
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("https://api.wheretheiss.at/v1/")
    };

public async Task<IEnumerable<Satellite>> GetSatellites()
    {
        var response = await Client.GetAsync("satellites");

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<Satellite>>(json) ?? Enumerable.Empty<Satellite>();
    }

    public async Task<SatelliteDetail> GetSingleSatellite(int id)
    {
        var response = await Client.GetAsync($"satellites/{id}&units=miles");
        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<SatelliteDetail>(json) ?? new SatelliteDetail();
    }

    public async Task<Location> GetLocation(decimal lat, decimal lon)
    {
        var response = await Client.GetAsync($"coordinates/{lat},{lon}");
        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Location>(json) ?? new Location();
    }
}
