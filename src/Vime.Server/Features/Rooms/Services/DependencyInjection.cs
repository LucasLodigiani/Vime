using Vime.Server.Features.Rooms.Services.Implementations;
using Vime.Server.Features.Rooms.Services.Interfaces;

namespace Vime.Server.Features.Rooms.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddRoomFeatureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAttachmentCategorizer, AttachmentCategorizer>();
        return services;
    }
}

