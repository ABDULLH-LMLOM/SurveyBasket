
using Microsoft.Extensions.Options;

namespace SurveyBasket.Api.Authentication;

public  class JwtProvider(IOptions<JwtOptions> options) 
{
    private readonly JwtOptions _options = options.Value;
    public (string token, int expiresIn) GenerateToken(User user)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key!)); 

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresIn = 30; 

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
            signingCredentials: signingCredentials 
            );

        return (new JwtSecurityTokenHandler().WriteToken(token), _options.ExpiryMinutes * 60);

    }
}