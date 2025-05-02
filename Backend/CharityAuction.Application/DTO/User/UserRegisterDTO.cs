namespace CharityAuction.Application.DTO.User
{
    public class UserRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; } = "https://cdn-icons-png.flaticon.com/512/2202/2202112.png";
        public string Role { get; set; } = "Seller";
    }
}

