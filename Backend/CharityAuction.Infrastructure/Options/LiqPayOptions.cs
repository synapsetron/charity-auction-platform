using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class LiqPayOptions
    {
        public const string SectionName = "LiqPay";

        [Required(ErrorMessage = "LiqPay PublicKey is required.")]
        public string PublicKey { get; set; } = string.Empty;

        [Required(ErrorMessage = "LiqPay PrivateKey is required.")]
        public string PrivateKey { get; set; } = string.Empty;

        [Required(ErrorMessage = "ResultUrl is required.")]
        [Url(ErrorMessage = "ResultUrl must be a valid URL.")]
        public string ResultUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "ServerUrl is required.")]
        [Url(ErrorMessage = "ServerUrl must be a valid URL.")]
        public string ServerUrl { get; set; } = string.Empty;
    }
}
