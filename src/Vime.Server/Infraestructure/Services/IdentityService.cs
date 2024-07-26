using Microsoft.AspNetCore.Identity;
using Vime.Server.Common.Interfaces;
using Vime.Server.Domain.Constants;
using Vime.Server.Domain.Entities;

namespace Vime.Server.Infraestructure.Services;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, Roles.User);
        return result.Succeeded;
    }

    public async Task<(bool, string, IEnumerable<string>)> CreateUserAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return (result.Succeeded, user.Id, result.Errors.Select(e => e.Description));
    }

    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user;
    }

    public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }

    public async Task<bool> VerifyCredentialsAsync(ApplicationUser user, string password)
    {
        bool result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }
}
