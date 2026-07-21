using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/Auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly ITokenService tokenService;

    public AuthController(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    [HttpGet("token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> GetToken([FromQuery] int userId, [FromQuery] string userRole)
    {
        return Ok(GenerateJSONWebToken(userId, userRole));
    }

    private string GenerateJSONWebToken(int userId, string userRole)
    {
        return tokenService.GenerateToken(userId, userRole);
    }
}
