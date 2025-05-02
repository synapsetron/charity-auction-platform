using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class ConnectionStringsOptions
    {
        public const string SectionName = "Database";

        [Required]
        public string DefaultConnection { get; set; } = string.Empty;
    }
}

