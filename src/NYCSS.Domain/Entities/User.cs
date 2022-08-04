using NYCSS.Utils.DomainObjects;

namespace NYCSS.Domain.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Photo { get; set; } = string.Empty;
    }
}