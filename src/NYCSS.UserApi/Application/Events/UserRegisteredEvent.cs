using NYCSS.Utils.MessageBus.Messages;

namespace NYCSS.UserApi.Application.Events
{
    public class UserRegisteredEvent : Event
    {
        public Guid ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Photo { get; set; } = string.Empty;

        public UserRegisteredEvent(Guid id, string username, string firstName, string lastName, string email, int age, string photo)
        {
            ID = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Photo = photo;
        }
    }
}