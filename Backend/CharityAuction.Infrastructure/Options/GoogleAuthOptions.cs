using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class GoogleAuthOptions
    {
        public const string SectionName = "GoogleSettings";

        [Required(ErrorMessage = "Google ClientId is required.")]
        public string ClientId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Google ClientSecret is required.")]
        public string ClientSecret { get; set; } = string.Empty;
    }
}
