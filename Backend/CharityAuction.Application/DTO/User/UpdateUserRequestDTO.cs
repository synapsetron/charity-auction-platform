namespace CharityAuction.Application.DTO.User
{
    public class UpdateUserRequestDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public string? PhotoUrl { get; set; }

        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}

