using CharityAuction.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Payment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [Required]
    public Guid AuctionId { get; set; }
    [ForeignKey("AuctionId")]
    public Auction Auction { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Currency { get; set; }

    public string Provider { get; set; }

    public string ExternalPaymentId { get; set; } // ID от платёжной системы

    public string Status { get; set; } // paid / failed / pending

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
}
