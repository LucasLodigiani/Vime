using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Vime.Server.Domain.Enums;
using Vime.Server.Infraestructure.Persistence;

namespace Vime.Server.Features.Rooms;

public record GetRoomsDto(int Id,string Title, string VideoUrl, Provider Provider,string Leader);
public class GetRooms : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    public GetRooms(ApplicationDbContext context){
        _context = context;
    }
    [HttpGet]
    [SwaggerOperation(Tags = new[] { "Rooms" })]
    public async Task<ActionResult<IEnumerable<GetRoomsDto>>> List()
    {
        var rooms = await _context.Rooms
        .Select(r => new GetRoomsDto(r.Id,r.Title,r.VideoUrl,r.Provider, r.Leader))
        .ToListAsync();

        return rooms;
    }
}
