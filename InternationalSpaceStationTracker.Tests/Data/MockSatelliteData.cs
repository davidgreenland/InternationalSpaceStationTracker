namespace InternationalSpaceStationTracker.Tests.Data;
public class MockSatelliteData
{
    public static string GetSatelliteData()
    {
        return "[{\"name\":\"testSatellite\",\"id\":12345}]";
    }

    public static string GetLargerSatelliteDataSet()
    {
        return "[{\"name\":\"testSatellite1\",\"id\":10001},{\"name\":\"testSatellite2\",\"id\":10002},{\"name\":\"testSatellite3\",\"id\":10003}]";
    }

    public static string GetEmptySatelliteDataSet()
    {
        return "[]";
    }
}
