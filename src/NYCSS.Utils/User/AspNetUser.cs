using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace NYCSS.Utils.User
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaims() => _accessor.HttpContext.User.Claims;

        public HttpContext GetHttpContext() => _accessor.HttpContext;

        public string GetUserEmail() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";

        public Guid GetUserId() => IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public string GetUsername() => IsAuthenticated() ? _accessor.HttpContext.User.GetUsername() : "";

        public string GetUserToken() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : "";

        public bool HasRole(string role) => _accessor.HttpContext.User.IsInRole(role);

        public bool IsAuthenticated() => _accessor.HttpContext.User.Identity.IsAuthenticated;
    }
}