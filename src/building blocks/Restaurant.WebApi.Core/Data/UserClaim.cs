namespace Restaurant.WebApi.Core.Data
{
    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }

        public UserClaim()
        {
            Value = string.Empty;
            Type = string.Empty;
        }
    }
}
