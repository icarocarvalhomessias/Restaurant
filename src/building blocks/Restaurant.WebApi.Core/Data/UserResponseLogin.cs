using Restaurant.WebApi.Core.Communication;

namespace Restaurant.WebApi.Core.Data
{
    public class UserResponseLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}
