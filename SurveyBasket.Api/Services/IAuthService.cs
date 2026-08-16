using SurveyBasket.Api.Contracts.Auth;

namespace SurveyBasket.Api.Services;

public interface IAuthService
{
    public Task<AuthResponse> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken);
}
