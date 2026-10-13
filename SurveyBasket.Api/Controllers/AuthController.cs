using Microsoft.Extensions.Options;

namespace SurveyBasket.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService , IOptions<JwtOptions> options) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly JwtOptions _options = options.Value;

    [HttpGet]
    public async Task<IActionResult> test(CancellationToken cancellationToken)
    {
        var response = _options.Issuer;
        return Ok(response);
    }
   

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] AuthRequest authRequest,  CancellationToken cancellationToken)
    {
        var response = await _authService.GetTokenAsync(authRequest.Email , authRequest.Password , cancellationToken); 
        return response is null ? BadRequest() : Ok(response) ;
    }
}
