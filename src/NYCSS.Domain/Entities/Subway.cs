namespace NYCSS.Domain.Entities
{
    public class Subway : Entity
    {
        public string Name { get; set; } = string.Empty;
        public Location Location { get; set; } = new();
        public string Street { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}