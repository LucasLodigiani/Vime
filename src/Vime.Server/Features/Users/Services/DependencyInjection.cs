using Vime.Server.Features.Users.Services.Implementations;
using Vime.Server.Features.Users.Services.Interfaces;

namespace Vime.Server.Features.Users.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersFeatureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}
