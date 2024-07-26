using System.Security.Claims;
using Vime.Server.Domain.Entities;

namespace Vime.Server.Features.Users.Services.Interfaces;

public interface IJwtService
{
    public string GenerateToken(ClaimsIdentity claimsForToken);
    public ClaimsIdentity GenerateClaims(ApplicationUser user, string role);
}
