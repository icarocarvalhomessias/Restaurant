using System.IdentityModel.Tokens.Jwt;

namespace Restaurant.Mobile.App.Services
{
    public class UserService
    {

        public UserService()
        {
            
        }

        public async Task SaveUserTokenAsync(string token)
        {
            var userToken = await GetUserTokenAsync();

            if (!string.IsNullOrEmpty(userToken))
            {
                SecureStorage.Remove("user_token");
            }

            await SecureStorage.SetAsync("user_token", token);
        }

        public async Task<string> GetUserTokenAsync()
        {
            return await SecureStorage.GetAsync("user_token");
        }

        public string GetClaimFromToken(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }

        public void Clear()
        {
            SecureStorage.Remove("user_token");
        }
    }
}
