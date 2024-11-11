using InternationalSpaceStationTracker.Services;
using InternationalSpaceStationTracker.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Net;

var httpRetryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryAsync(6, retryAttempt =>
        {
            Console.WriteLine($"Retry {retryAttempt}");
            return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        });

var services = new ServiceCollection();
services.AddHttpClient("issHttpClient", x => x.BaseAddress = new Uri("https://api.wheretheiss.at/v1/"));
services.AddTransient<ISatelliteService, SatelliteService>();
services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>(httpRetryPolicy);
var serviceProvider = services.BuildServiceProvider();

var satelliteService = serviceProvider.GetService<ISatelliteService>();

var satellites = await satelliteService.GetSatellites();
//var iss = await satelliteService.GetSingleSatellite(satellites.First(x => x.IsIss()).Id);
var iss = await satelliteService.GetSingleSatellite(9999);

Console.WriteLine(iss);

if (iss != null)
{
    var location = await satelliteService.GetLocation(iss.Latitude, iss.Longitude);
    Console.WriteLine(location);
}

Console.ReadKey();

