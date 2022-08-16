using Newtonsoft.Json;

using NYCSS.Domain.Entities;
using NYCSS.Infra.JsonReader.Models;

namespace NYCSS.Infra.JsonReader.Services
{
    public class JsonReaderService
    {
        public IEnumerable<Subway> ConvertToSubwayStations(GeoJson geoJson)
        {
            var subwayStations = new List<Subway>();

            geoJson.Features!.ToList().ForEach(x =>
            {
                var subway = new Subway
                {
                    Name = x.Properties.Name!,
                    Location = new()
                    {
                        Latitude = x.Geometry.Coordinates[0],
                        Longitude = x.Geometry.Coordinates[1]
                    },
                    Line = x.Properties.Line!
                };

                subwayStations.Add(subway);
            });

            return subwayStations;
        }

        public GeoJson GetGeoJson()
        {
            GeoJson geoJson = new();

            using (StreamReader sr = new("subwayStations.json"))
            {
                string json = sr.ReadToEnd();
                geoJson = JsonConvert.DeserializeObject<GeoJson>(json);
            }

            return geoJson;
        }
    }
}