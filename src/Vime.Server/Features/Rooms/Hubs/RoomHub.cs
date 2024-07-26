using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Vime.Server.Common.Interfaces;
using Vime.Server.Entities;
using Vime.Server.Infraestructure.Persistence;

namespace Vime.Server.Common.Hubs;

[Authorize]
public partial class ApplicationHub
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public ApplicationHub(ApplicationDbContext context, ICurrentUserService currentUserService){
        _context = context;
        _currentUserService = currentUserService;
    }
    //Refactorizar esto
    private readonly static List<RoomClient> roomClients = new List<RoomClient>();
    public async Task JoinToRoom(string roomId)
    {
        string userId = _currentUserService.UserId;
        string userName = _currentUserService.UserName;
        roomClients.Add(new RoomClient(userId, userName,roomId, Context.ConnectionId));
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
    }


    public async Task SendVideoEvent(string videoEvent, string roomId)
    {
        await Clients.Group(roomId).SendAsync("ReceiveVideoEvent", videoEvent);
    }

    public async Task SendMessage(string message){
        var room = roomClients.Where(x => x.ConnectionId == Context.ConnectionId).First();
        await Clients.Groups(room.RoomId).SendAsync("ReceiveChatMessage", room.ClientUsername, message);
    }
}
