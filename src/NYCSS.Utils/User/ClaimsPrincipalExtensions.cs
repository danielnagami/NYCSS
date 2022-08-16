using System.Security.Claims;

namespace NYCSS.Utils.User
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUsername(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
    }
}