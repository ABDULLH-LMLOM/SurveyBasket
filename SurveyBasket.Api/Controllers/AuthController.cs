using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Contracts.Authentication;

namespace SurveyBasket.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] AuthRequest authRequest,  CancellationToken cancellationToken)
    {
        var response = await _authService.GetTokenAsync(authRequest.Email , authRequest.Password , cancellationToken); 
        return response is null ? BadRequest() : Ok(response) ;
    }
}
