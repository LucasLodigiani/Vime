namespace Vime.Server.Common.Interfaces;

public interface IDateTime
{
    DateTimeOffset Now { get; }
}