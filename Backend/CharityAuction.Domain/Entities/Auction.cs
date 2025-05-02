using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityAuction.Domain.Entities
{
    public class Auction
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters.")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "ImageUrl must be a valid URL.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Starting price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Starting price must be a non-negative number.")]
        public decimal StartingPrice { get; set; }

        public decimal? FinalPrice { get; set; }

        public bool IsSold { get; set; } = false;

        [Required(ErrorMessage = "Organizer ID is required.")]
        public string OrganizerId { get; set; }

        [ForeignKey("OrganizerId")]
        public ApplicationUser Organizer { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public DateTime EndTime { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();

        public bool IsApproved { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
