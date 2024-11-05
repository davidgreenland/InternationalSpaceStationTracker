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

        var result = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<Satellite>>(result) ?? Enumerable.Empty<Satellite>();
    }

    public async Task<SatelliteDetail> GetSingleSatellite(int id)
    {
        //var path = HttpUtility.ParseQueryString($"https://api.wheretheiss.at/v1/satellites/{id}");
        //path["units"] = "miles";
        //Console.WriteLine(path.ToString());
        var response = await Client.GetAsync($"satellites/{id}&units=miles");

        var result = await response.Content.ReadAsStringAsync();

        var a = JsonSerializer.Deserialize<SatelliteDetail>(result);

        return a;
    }

    public async Task<Location>
}
