using InternationalSpaceStationTracker.Services;

var httpClient = new HttpClient();
var satelliteService = new SatelliteService(httpClient);
var satellites = await satelliteService.GetSatellites();
var iss = await satelliteService.GetSingleSatellite(satellites.First().Id);
var location = satelliteService.GetLocation(iss.Latitude, iss.Longitude);

Console.WriteLine(iss);
Console.WriteLine(await location);

Console.ReadKey();