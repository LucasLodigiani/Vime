using Vime.Server.Domain.Enums;

namespace Vime.Server.Features.Rooms.Services.Interfaces;

public interface IAttachmentCategorizer
{
    public Provider CategorizeUrlAndGetProvider(string videoUrl);
}
