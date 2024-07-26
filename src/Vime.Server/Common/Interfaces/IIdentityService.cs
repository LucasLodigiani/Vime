using Vime.Server.Domain.Entities;

namespace Vime.Server.Common.Interfaces;

public interface IIdentityService
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);

    Task<(bool, string, IEnumerable<string> errors)> CreateUserAsync(ApplicationUser user, string password);

    Task<bool> VerifyCredentialsAsync(ApplicationUser user, string password);

    Task<IList<string>> GetUserRolesAsync(ApplicationUser user);

    Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName);
}
