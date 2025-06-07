using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class FondyPayOptions
    {
        public const string SectionName = "Fondy";

        [Required(ErrorMessage = "MerchantId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MerchantId must be greater than 0.")]
        public int MerchantId { get; set; }

        [Required(ErrorMessage = "SecretKey is required.")]
        public string SecretKey { get; set; } = string.Empty;

        [Required(ErrorMessage = "ResultUrl is required.")]
        [Url(ErrorMessage = "ResultUrl must be a valid URL.")]
        public string ResultUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "ServerUrl is required.")]
        [Url(ErrorMessage = "ServerUrl must be a valid URL.")]
        public string ServerUrl { get; set; } = string.Empty;
    }
}
