namespace SurveyBasket.Api.Services;

public class AuthService (UserManager<User> userManager , JwtProvider jwtProvider): IAuthService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly JwtProvider _jwtProvider = jwtProvider;
    public async Task<AuthResponse> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return null;
        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
        if (!isValidPassword)
            return null;

        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        return new AuthResponse(user.Id, user.Email!,user.FirstName!, user.LastName!, token, expiresIn ); 
    }
}
