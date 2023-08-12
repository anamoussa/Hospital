using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService service;

    public AuthController(IAuthService _service)
    {
        service = _service;
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.RegisterAsync(model);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        return Ok(result);
    }
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.LoginAsync(model);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        if (!string.IsNullOrEmpty(result.RefreshToken))
            SetRefreshTokenToCookie(result.RefreshToken, result.RefreshTokenExpiration);
        return Ok(result);
    }
    [HttpPost("addToRole")]
    public async Task<IActionResult> AddToRoleAsync([FromBody] RoleModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await service.AddToRoleAsync(model);
        if (!string.IsNullOrEmpty(result))
            return BadRequest(result);
        return Ok(result + "Added");
    }
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        var rftoken = Request.Cookies["rftoken"];
        if (string.IsNullOrEmpty(rftoken))
            return BadRequest("invalid token!");
        var result = await service.RefreshTokenAsync(rftoken);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        SetRefreshTokenToCookie(result.RefreshToken!, result.RefreshTokenExpiration);
        return Ok(result);
    }
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeTokenAsync(string? token)
    {
        var rftoken = token ?? Request.Cookies["rftoken"];
        if (string.IsNullOrEmpty(rftoken))
            return BadRequest("token required!");
        var result = await service.RevokeTokenAsync(rftoken!);
        return result ? Ok("token revoked successfully!") : BadRequest("invalid token!");
    }
    private void SetRefreshTokenToCookie(string rftoken, DateTime expires)
    {
        var Options = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires.ToLocalTime()
        };
        Response.Cookies.Append("rftoken", rftoken, options: Options);
    }
}