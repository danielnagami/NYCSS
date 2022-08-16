using NYCSS.Domain.Entities;
using NYCSS.Infra.JsonReader.Models;

namespace NYCSS.Infra.JsonReader.Interfaces
{
    public interface IJsonReaderService
    {
        GeoJson GetGeoJson();
        IEnumerable<Subway> ConvertToSubwayStations(GeoJson geoJson);
    }
}