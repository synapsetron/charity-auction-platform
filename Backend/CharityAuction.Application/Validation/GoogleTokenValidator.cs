using CharityAuction.Application.Interfaces;
using Google.Apis.Auth;

public class GoogleTokenValidator : IGoogleTokenValidator
{
    public Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken)
    {
        return GoogleJsonWebSignature.ValidateAsync(idToken);
    }
}
