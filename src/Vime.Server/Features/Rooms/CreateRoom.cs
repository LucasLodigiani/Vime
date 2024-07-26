using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Vime.Server.Common.Interfaces;
using Vime.Server.Domain.Enums;
using Vime.Server.Entities;
using Vime.Server.Features.Rooms.Services.Interfaces;
using Vime.Server.Infraestructure.Persistence;

namespace Vime.Server.Features.Rooms;

public record CreateRoomRequest(string Title, string VideoUrl);
public record CreateRoomResponse(string RoomId, string Title, string VideoUrl, Provider Provider);
public class CreateRoom : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IAttachmentCategorizer _attachmentCategorizer;
    private readonly ICurrentUserService _currentUserService;
    public CreateRoom(ApplicationDbContext context, IAttachmentCategorizer attachmentCategorizer, ICurrentUserService currentUserService)
    {
        _context = context;
        _attachmentCategorizer = attachmentCategorizer;
        _currentUserService = currentUserService;
    }
    [HttpPost]
    [Authorize]
    [SwaggerOperation(Tags = new[] { "Rooms" })]
    public async Task<ActionResult<CreateRoomResponse>> Create(CreateRoomRequest request)
    {
        Provider provider = _attachmentCategorizer.CategorizeUrlAndGetProvider(request.VideoUrl);
        if(provider == Provider.None) return BadRequest("Your video doesn't match any provider!");
        
        string leaderName = _currentUserService.UserName;

        var room = new Room{ Title = request.Title, VideoUrl = request.VideoUrl, Provider = provider, Leader = leaderName };

        await _context.AddAsync(room);
        await _context.SaveChangesAsync();

        return Ok(new CreateRoomResponse(room.Id.ToString(), room.Title, room.VideoUrl, room.Provider));
    }
}
