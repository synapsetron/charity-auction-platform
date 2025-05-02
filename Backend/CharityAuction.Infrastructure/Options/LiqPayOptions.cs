using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class LiqPayOptions
    {
        public const string SectionName = "LiqPay";

        [Required]
        public string PublicKey { get; set; } = string.Empty;

        [Required]
        public string PrivateKey { get; set; } = string.Empty;

        [Required]
        public string ResultUrl { get; set; } = string.Empty;

        [Required]
        public string ServerUrl { get; set; } = string.Empty;
    }
}
