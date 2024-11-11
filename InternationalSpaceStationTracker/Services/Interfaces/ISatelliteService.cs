using InternationalSpaceStationTracker.Models;

namespace InternationalSpaceStationTracker.Services.Interfaces
{
    public interface ISatelliteService
    {
        Task<Location?> GetLocation(decimal lat, decimal lon);
        Task<IEnumerable<Satellite>> GetSatellites();
        Task<SatelliteDetail?> GetSingleSatellite(int id);
    }
}