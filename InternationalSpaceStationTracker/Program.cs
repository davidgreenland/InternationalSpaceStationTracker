using InternationalSpaceStationTracker.Models;
using InternationalSpaceStationTracker.Services;

var httpClient = new HttpClient();
var satelliteService = new SatelliteService(httpClient);
var satellites = await satelliteService.GetSatellites();
var iss = await satelliteService.GetSingleSatellite(satellites.First().Id);
Console.WriteLine(iss);

Location location;
if (iss != null)
{
    location = await satelliteService.GetLocation(iss.Latitude, iss.Longitude);
    Console.WriteLine(location);
}

Console.ReadKey();