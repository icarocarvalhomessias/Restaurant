namespace Restaurant.WebApi.Core.Data
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }

        public UserToken()
        {
            Id = string.Empty;
            Email = string.Empty;
            Claims = new List<UserClaim>();
        }
    }
}
