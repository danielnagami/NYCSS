using FluentValidation;

using NYCSS.Utils.MessageBus.Messages;

namespace NYCSS.UserApi.Application.Commands
{
    public class RegisterUserCommand : Command
    {
        public Guid ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Photo { get; set; } = string.Empty;

        public RegisterUserCommand(Guid id, string username, string firstName, string lastName, string email, int age, string photo)
        {
            ID = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Photo = photo;
        }

        public override bool Valid()
        {
            ValidationResult = new RegisterUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterUserValidation : AbstractValidator<RegisterUserCommand>
        {
            public RegisterUserValidation()
            {
                RuleFor(c => c.ID)
                    .NotEqual(Guid.Empty)
                    .WithMessage("User's ID invalid");

                RuleFor(c => c.Username)
                   .NotEmpty()
                   .WithMessage("Username not valid");

                RuleFor(c => c.FirstName)
                   .NotEmpty()
                   .WithMessage("FirstName not valid");

                RuleFor(c => c.LastName)
                   .NotEmpty()
                   .WithMessage("LastName not valid");

                RuleFor(c => c.Email)
                   .NotEmpty()
                   .WithMessage("Email not valid");

                RuleFor(c => c.Age)
                   .NotEmpty()
                   .GreaterThan(0)
                   .WithMessage("Age not valid");
            }
        }
    }
}