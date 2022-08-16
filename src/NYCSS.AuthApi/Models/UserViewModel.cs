using System.ComponentModel.DataAnnotations;

namespace NYCSS.AuthApi.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [EmailAddress(ErrorMessage = "The field {0} is in invalid format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int Age { get; set; }

        public string Photo { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Compare(nameof(Password), ErrorMessage = "The passwords doesn't match.")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Password { get; set; } = string.Empty;
    }

    public class UserLoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public double ExpiresIn { get; set; }
        public UserToken? UserToken { get; set; }
    }

    public class UserToken
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public IEnumerable<UserClaim>? Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}