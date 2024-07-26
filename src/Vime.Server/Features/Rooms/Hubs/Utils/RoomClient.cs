namespace Vime.Server.Common.Hubs;

public class RoomClient
{
    public RoomClient(string clientId, string clientUsername,string roomId, string connectionId){
        RoomId = roomId;
        ClientId = clientId;
        ClientUsername = clientUsername;
        ConnectionId = connectionId;
    }
    public string ClientId { get; private set; }
    public string ClientUsername {get; private set;}
    public string RoomId { get; private set; }
    public string ConnectionId {get; private set;}
}
