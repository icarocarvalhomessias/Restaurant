using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.WebApi.Core.Controller;
using Restaurant.WebApi.Core.Data;
using Restaurant.WebApi.Core.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Auth.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;

        public AuthController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        
        [HttpPost("new-account")]
        public async Task<IActionResult> Registrar(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email,
                EmailConfirmed = true
            };

            var results = await _userManager.CreateAsync(user, userRegistration.Password);

            if (results.Succeeded)
            {
                //var familiarResult = await RegistrarUsuario(userRegistration);

                //if (!familiarResult.ValidationResult.IsValid)
                //{
                //    await _userManager.DeleteAsync(user);
                //    return CustomResponse(familiarResult.ValidationResult);
                //}

                var role = userRegistration.TypeUser == TypeUser.Admin ? "Admin" : "DeliveryMan";
                await _userManager.AddToRoleAsync(user, role);

                return CustomResponse(await CreateJwt(userRegistration.Email));
            }

            foreach (var error in results.Errors)
            {
                AddErrorProcessing(error.Description);
            }

            return CustomResponse();
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await CreateJwt(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddErrorProcessing("User temporarily blocked due to invalid attempts");
                return CustomResponse();
            }

            AddErrorProcessing("Incorrect username or password");
            return CustomResponse();

        }

        private async Task<UserResponseLogin> CreateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
            claims.Add(new Claim("UserName", user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Broadcaster,
                Audience = _appSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireAt),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var endedToken = tokenHandler.WriteToken(token);

            var response = new UserResponseLogin
            {
                AccessToken = endedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpireAt).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
