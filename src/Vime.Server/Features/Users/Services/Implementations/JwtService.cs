using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Vime.Server.Domain.Entities;
using Vime.Server.Features.Users.Services.Interfaces;

namespace Vime.Server.Features.Users.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ClaimsIdentity GenerateClaims(ApplicationUser user, string role)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
        claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        claims.AddClaim(new Claim(ClaimTypes.Role, role));
            

        return claims;
    }

    public string GenerateToken(ClaimsIdentity claimsForToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsForToken,
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
