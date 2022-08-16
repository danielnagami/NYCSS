namespace NYCSS.Infra.JsonReader.Models
{
    public class GeoJson
    {
        public string? Type { get; set; }
        public IEnumerable<Feature>? Features { get; set; }
    }
}