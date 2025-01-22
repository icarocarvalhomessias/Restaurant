namespace Restaurant.WebApi.Core.Identity
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpireAt { get; set; }
        public string Broadcaster { get; set; }
        public string ValidOn { get; set; }
    }
}
