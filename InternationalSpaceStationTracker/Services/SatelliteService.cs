using InternationalSpaceStationTracker.Models;
using InternationalSpaceStationTracker.Services.Interfaces;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Net;
using System.Text.Json;

namespace InternationalSpaceStationTracker.Services;

public class SatelliteService : ISatelliteService
{
    private readonly HttpClient _httpClient;
    private readonly IAsyncPolicy<HttpResponseMessage> _asyncRetryPolicy;

    public SatelliteService(IHttpClientFactory httpClientFactory, IAsyncPolicy<HttpResponseMessage> asyncRetryPolicy)
    {
        _httpClient = httpClientFactory.CreateClient("issHttpClient");
        _asyncRetryPolicy = asyncRetryPolicy;
    }

    public async Task<IEnumerable<Satellite>> GetSatellites()
    {
        var response = await _asyncRetryPolicy.ExecuteAsync(() => _httpClient.GetAsync("satellites"));
        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<Satellite>>(json) ?? Enumerable.Empty<Satellite>();
    }

    public async Task<SatelliteDetail?> GetSingleSatellite(int id)
    {
        var response = await _asyncRetryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"satellites/{id}?units=miles"));
        var json = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine(JsonSerializer.Deserialize<ErrorReponse>(json));
            return null;
        }

        return JsonSerializer.Deserialize<SatelliteDetail>(json) ?? new SatelliteDetail();
    }

    public async Task<Location?> GetLocation(decimal lat, decimal lon)
    {
        var response = await _asyncRetryPolicy.ExecuteAsync(() => _httpClient.GetAsync($"coordinates/{lat},{lon}"));
        var json = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            Console.WriteLine(JsonSerializer.Deserialize<ErrorReponse>(json));
            return null;
        }

        return JsonSerializer.Deserialize<Location>(json) ?? new Location();
    }
}
