using AuthMicroservice.Models;
using AuthMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private static readonly IReadOnlyCollection<User> Users = new List<User>
    {
        new() { Username = "admin", Password = "admin123", Role = "Admin" },
        new() { Username = "user", Password = "user123", Role = "User" }
    };

    private readonly JwtService jwtService;

    public AuthController(JwtService jwtService)
    {
        this.jwtService = jwtService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<object>> Login([FromBody] LoginModel model)
    {
        var user = await ValidateUserAsync(model);
        if (user is null)
        {
            return Unauthorized();
        }

        var token = jwtService.GenerateToken(user);
        return Ok(new { Token = token, user.Username, user.Role });
    }

    private static Task<User?> ValidateUserAsync(LoginModel model)
    {
        var user = Users.FirstOrDefault(candidate =>
            string.Equals(candidate.Username, model.Username, StringComparison.OrdinalIgnoreCase) &&
            candidate.Password == model.Password);

        return Task.FromResult(user);
    }
}
