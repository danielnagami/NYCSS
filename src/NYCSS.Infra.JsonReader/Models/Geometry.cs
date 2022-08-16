namespace NYCSS.Infra.JsonReader.Models
{
    public class Geometry
    {
        public string? Type { get; set; }
        public float[] Coordinates { get; set; } = new float[0];
    }
}