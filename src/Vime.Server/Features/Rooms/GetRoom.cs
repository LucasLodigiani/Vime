using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vime.Server.Domain.Enums;
using Vime.Server.Infraestructure.Persistence;

namespace Vime.Server;

public record GetRoomResponse(int roomId, string VideoUrl,string Leader, Provider Provider);
public class GetRoom : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    public GetRoom(ApplicationDbContext context){
        _context = context;
    }
    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { "Rooms" })]
    public async Task<ActionResult<GetRoomResponse>> Get([FromRoute]int id){
        var room = await _context.Rooms.FindAsync(id);
        if(room is null) return NotFound();
        
        return Ok(new GetRoomResponse(room.Id, room.VideoUrl, room.Leader, room.Provider));
    }

}
