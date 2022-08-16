namespace NYCSS.Infra.JsonReader.Models
{
    public class Feature
    {
        public string? Type { get; set; }
        public Properties Properties { get; set; } = new Properties();
        public Geometry Geometry { get; set; } = new();
    }
}