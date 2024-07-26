using Vime.Server.Domain.Enums;
using Vime.Server.Features.Rooms.Services.Interfaces;

namespace Vime.Server.Features.Rooms.Services.Implementations;

public class AttachmentCategorizer : IAttachmentCategorizer
{
    private readonly string[] _supportedExternalProviderExtensions = { ".mp4", ".mkv", ".webm"};
    public Provider CategorizeUrlAndGetProvider(string videoUrl)
    {
        if(IsYoutubeLink(videoUrl))
        {
            return Provider.Youtube;
        }
        else if(IsOtherProvider(videoUrl))
        {
            return Provider.Other;
        }
        else
        {
            return Provider.None;
        }
    }

    private bool IsYoutubeLink(string videoUrl)
    {
        try{
            Uri uri = new Uri(videoUrl);
            string host = uri.Host.ToLower();
            if (host == "www.youtube.com" || host == "www.youtube.be") return true;
        }catch{
            return false;
        }
        return false;
    }

    private bool IsOtherProvider(string videoUrl)
    {
        if(_supportedExternalProviderExtensions.Any(ext => ext.Equals(Path.GetExtension(videoUrl)))) return true;
        return false;
    }
}
