using Vime.Server.Domain.Common;
using Vime.Server.Domain.Enums;

namespace Vime.Server.Entities;

public class Room : BaseEntity
{
    public required string Title { get; set;}
    public required string VideoUrl {get; set;}
    public Provider Provider {get; set;}
    public required string Leader { get; set;} 

}
