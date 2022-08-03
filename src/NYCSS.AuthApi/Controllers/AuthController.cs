using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using NYCSS.AuthApi.Interfaces;
using NYCSS.AuthApi.Models;
using NYCSS.AuthApi.Models.Messages;
using NYCSS.Utils.Controllers;
using NYCSS.Utils.Identity;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

namespace NYCSS.AuthApi.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfigurationAppSettings _appSettings;
        private readonly IMessageBus _bus;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, JwtConfigurationAppSettings appSettings, IMessageBus bus)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings;
            _bus = bus;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var clienteResult = await RegisterClient(userRegister);

                if (!clienteResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return CustomResponse(clienteResult.ValidationResult);
                }

                return CustomResponse(await GenerateJWT(userRegister.Email));
            }

            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJWT(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddError("User temporaly blocked by invalid tries.");
                return CustomResponse();
            }

            AddError("User and/or password invalid.");
            return CustomResponse();
        }

        private async Task<UserLoginResponse> GenerateJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetUserClaims(claims, user);
            var encodedToken = EncodeToken(identityClaims);

            return GetTokenResponse(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserLoginResponse GetTokenResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserLoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = Guid.Parse(user.Id),
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private async Task<ResponseMessage> RegisterClient(UserRegister userRegister)
        {
            var usuario = await _userManager.FindByEmailAsync(userRegister.Email);

            var userRegistered = new UserRegisteredIntegrationEvent(
                Guid.Parse(usuario.Id), 
                userRegister.Username, 
                userRegister.FirstName, 
                userRegister.LastName, 
                userRegister.Email, 
                userRegister.Age, 
                userRegister.Photo);

            try
            {
                return await _bus.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage>(userRegistered);

            }
            catch (Exception)
            {
                await _userManager.DeleteAsync(usuario);
                throw;
            }
        }
    }
}