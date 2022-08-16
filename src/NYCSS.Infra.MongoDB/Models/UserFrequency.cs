using MongoDB.Bson;

using System.Text.Json.Serialization;

namespace NYCSS.Infra.MongoDB.Models
{
    public class UserFrequency
    {
        public UserFrequency(string username, Dictionary<string, int> subwayFrequency)
        {
            Username = username;
            SubwayFrequency = subwayFrequency;
        }

        public UserFrequency()
        {

        }

        [JsonIgnore]
        public ObjectId _id { get; set; }
        public string Username { get; set; } = string.Empty;
        public Dictionary<string, int> SubwayFrequency { get; set; } = new Dictionary<string, int>();
    }
}