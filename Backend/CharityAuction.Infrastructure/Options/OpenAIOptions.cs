using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class OpenAIOptions
    {
        public const string SectionName = "OpenAI";

        [Required(ErrorMessage = "OpenAI ApiKey is required.")]
        public string ApiKey { get; set; } = string.Empty;
    }
}
