namespace CharityAuction.Application.DTO.User
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Role { get; set; }
        public decimal CommissionBalance { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}

