using Vime.Server.Common.Interfaces;

namespace Vime.Server.Infraestructure.Services;

public class DateTimeService : IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}