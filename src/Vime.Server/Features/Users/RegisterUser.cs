using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vime.Server.Common.Interfaces;
using Vime.Server.Domain.Constants;
using Vime.Server.Domain.Entities;

namespace Vime.Server;

public record RegisterUserRequest(string Username, string Password);
public class RegisterUser : ApiControllerBase
{
    private readonly IIdentityService _identityService;
    public RegisterUser(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    [HttpPost]
    [SwaggerOperation(Tags = new[] { "Auth" })]
    public async Task<ActionResult> Register(RegisterUserRequest request)
    {
        var user = new ApplicationUser { UserName = request.Username };

        var (result,  userId, errors) = await _identityService.CreateUserAsync(user, request.Password);

        if(!result){
            foreach(var error in errors){
                ModelState.TryAddModelError("Error",error);
            }
            return UnprocessableEntity(ModelState);
        }
        await _identityService.AddUserToRoleAsync(user, Roles.User);

        return Ok();
    }
}
