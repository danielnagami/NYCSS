using NYCSS.Utils.DomainObjects;

namespace NYCSS.Domain.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public User(Guid id, string username, string firstName, string lastName, string email, int age, string photo)
        {
            ID = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Photo = photo;
        }
        public User()
        {

        }

        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Photo { get; set; } = string.Empty;
    }
}