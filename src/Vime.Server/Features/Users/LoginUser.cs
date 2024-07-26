using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vime.Server.Common.Interfaces;
using Vime.Server.Features.Users.Services.Interfaces;

namespace Vime.Server.Features.Users;
public record LoginRequest(string Username, string Password);
public record LoginResponse(string Username, string Role, string jwt);

public class LoginUser : ApiControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;
    public LoginUser(IIdentityService identityService, IJwtService jwtService)
    {
        _identityService = identityService;
        _jwtService = jwtService;
    }
    [HttpPost]
    [SwaggerOperation(Tags = new[] { "Auth" })]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _identityService.GetUserByNameAsync(request.Username);

        if(user is null) return Unauthorized("User not found!");

        if (!await _identityService.VerifyCredentialsAsync(user, request.Password)) return Unauthorized("Wrong username or password");

        var role = (await _identityService.GetUserRolesAsync(user)).First();

        var claimsForToken = _jwtService.GenerateClaims(user, role);

        var token = _jwtService.GenerateToken(claimsForToken);

        //user.RefreshToken = Guid.NewGuid().ToString();

        //await _userManager.UpdateAsync(user);

        //Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
        //Response.Cookies.Append("X-Username", user.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
        //Response.Cookies.Append("X-Refresh-Token", user.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

        return Ok(new LoginResponse(user.UserName, role, token));
    }
}
