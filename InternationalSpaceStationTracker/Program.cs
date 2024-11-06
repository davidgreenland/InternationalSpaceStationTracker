using InternationalSpaceStationTracker.Services;

var httpClient = new SatelliteService();
var satellites = await httpClient.GetSatellites();
var iss = await httpClient.GetSingleSatellite(satellites.First().Id);
var location = httpClient.GetLocation(iss.Latitude, iss.Longitude);

Console.WriteLine(iss);
Console.WriteLine(await location);
Console.ReadKey();