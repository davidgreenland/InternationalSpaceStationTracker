using InternationalSpaceStationTracker.Services;

var httpClient = new HttpClient();
var satelliteService = new SatelliteService(httpClient);
var satellites = await satelliteService.GetSatellites();
var iss = await satelliteService.GetSingleSatellite(satellites.First(x => x.IsIss()).Id);
Console.WriteLine(iss);

if (iss != null)
{
    var location = await satelliteService.GetLocation(iss.Latitude, iss.Longitude);
    Console.WriteLine(location);
}

Console.ReadKey();