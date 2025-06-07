using System;
using System.ComponentModel.DataAnnotations;


namespace CharityAuction.Infrastructure.Options
{
    public class OpenAIOptions
    {
        [Required]
        public const string SectionName = "OpenAI";

        [Required]
        public string ApiKey { get; set; } = string.Empty;
    }
}
