namespace InternationalSpaceStationTracker.Tests.Data;
public class MockSatelliteData
{
    public static string GetSatellite()
    {
        return """
                [
                    {
                        "name":"testSatellite",
                        "id":12345
                    }
                ]
                """;
    }

    public static string GetLargerSatelliteSet()
    {
        return """
                [
                    {
                        "name":"testSatellite1",
                        "id":10001
                    },
                    {
                        "name":"testSatellite2",
                        "id":10002
                    },
                    {
                        "name":"testSatellite3",
                        "id":10003
                    }
                ]
                """;
    }

    public static string GetEmptySatelliteSet()
    {
        return "[]";
    }

    public static string GetSingleSatellite()
    {
        return """
                {
                    "name":"testSatellite",
                    "id":10001,
                    "latitude":-16.0123456789,
                    "longitude":-125.0123456789,
                    "altitude":257.0123456789,
                    "velocity":17145.0123456789,
                    "visibility":"daylight",
                    "footprint":2781.0123456789,
                    "timestamp":1730123456,
                    "daynum":2460621.1234567,
                    "solar_lat":-16.012345678901,
                    "solar_lon":293.01234567890,
                    "units":"miles"
                }
                """;
    }

    public static string GetInvalidSatelliteID()
    {
        return """
        {
            "error": "satellite not found",
            "status": 404
        }
        """;
    }
}
