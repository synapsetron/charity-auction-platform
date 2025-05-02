namespace CharityAuction.Application.DTO.User
{
    public class ConfirmEmailRequestDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
