using MongoDB.Driver;

using NYCSS.Domain.Entities;
using NYCSS.Infra.JsonReader.Services;

namespace NYCSS.Infra.MongoDB.Data
{
    public static class SubwaySeed
    {
        public static void Seed(IMongoCollection<Subway> subwayCollection)
        {
            var documentExists = subwayCollection.Find(_ => true).Any();
            if (!documentExists)
            {
                var jsonReader = new JsonReaderService();
                var geoJson = jsonReader.GetGeoJson();
                var subways = jsonReader.ConvertToSubwayStations(geoJson);

                subwayCollection.InsertMany(subways);
            }
        }
    }
}