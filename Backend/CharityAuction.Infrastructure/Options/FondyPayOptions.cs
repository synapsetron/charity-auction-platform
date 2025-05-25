
using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class FondyPayOptions
    {
        public const string SectionName = "Fondy";

        [Required]
        public int MerchantId { get; set; }

        [Required]
        public string SecretKey { get; set; } = string.Empty;

        [Required]
        public string ResultUrl { get; set; } = string.Empty;

        [Required]
        public string ServerUrl { get; set; } = string.Empty;
    }
}
