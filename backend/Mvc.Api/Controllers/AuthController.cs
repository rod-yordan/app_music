using DtoModel.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Business.Auth;

namespace Mvc.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthBusiness _authBusiness;

    public AuthController(IAuthBusiness authBusiness)
    {
        _authBusiness = authBusiness;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authBusiness.Login(request);

        if (response == null)
            return Unauthorized(new { message = "Credenciales incorrectas" });

        return Ok(response);
    }
}