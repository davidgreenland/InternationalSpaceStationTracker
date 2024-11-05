using InternationalSpaceStationTracker.Services;

var httpClient = new SatelliteService();

var satellites = await httpClient.GetSatellites();

//foreach (var satellite in satellites)
//{
//    Console.WriteLine(satellite);
//}

var iss = await httpClient.GetSingleSatellite(satellites.First().Id);

Console.WriteLine(iss);

var location = await httpClient.GetLocation(iss.Latitude, iss.Longitude);

Console.WriteLine(location);

Console.ReadKey();