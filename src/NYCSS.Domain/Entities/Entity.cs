namespace NYCSS.Domain.Entities
{
    public abstract class Entity
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}