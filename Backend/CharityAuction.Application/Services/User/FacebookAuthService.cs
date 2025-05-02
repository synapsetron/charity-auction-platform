
using CharityAuction.Application.DTO.User.FacebookUser;
using CharityAuction.Application.Interfaces.User;
using System.Text.Json;


namespace CharityAuction.Application.Services.User
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly HttpClient _httpClient;

        public FacebookAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FacebookUserInfoDTO> ValidateAccessTokenAsync(string accessToken)
        {
            var verifyTokenEndpoint = $"https://graph.facebook.com/me?access_token={accessToken}&fields=id,email,first_name,last_name,picture.width(100).height(100)";

            var response = await _httpClient.GetAsync(verifyTokenEndpoint);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var fbUser = JsonSerializer.Deserialize<FacebookUserInfoDTO>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return fbUser;
        }
    }
}
