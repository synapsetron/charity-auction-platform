
namespace CharityAuction.Infrastructure.Options
{
    public class GoogleAuthOptions
    {
        public const string SectionName = "GoogleSettings";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
