using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name must be between 1 and 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name must be between 1 and 50 characters.")]
        public string LastName { get; set; }

        [Url(ErrorMessage = "PhotoUrl must be a valid URL.")]
        public string PhotoUrl { get; set; } = "https://cdn-icons-png.flaticon.com/512/2202/2202112.png";

        [Required]
        [StringLength(20, ErrorMessage = "Role must be between 1 and 20 characters.")]
        public string Role { get; set; } = UserRole.Seller;

        [Range(0, double.MaxValue, ErrorMessage = "Commission balance cannot be negative.")]
        public decimal CommissionBalance { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; } = 0;

        public ICollection<Auction> Auctions { get; set; } = new List<Auction>();

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
