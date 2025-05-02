using CharityAuction.Application.DTO.User.FacebookUser;
using System.Threading.Tasks;

namespace CharityAuction.Application.Interfaces.User
{
    public interface IFacebookAuthService
    {
        Task<FacebookUserInfoDTO> ValidateAccessTokenAsync(string accessToken);
    }
}
