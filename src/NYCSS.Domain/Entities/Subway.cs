using MongoDB.Bson;

using System.Text.Json.Serialization;

namespace NYCSS.Domain.Entities
{
    public class Subway : Entity
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Location Location { get; set; } = new();
        public string Street { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Line { get; set; } = string.Empty;
    }
}