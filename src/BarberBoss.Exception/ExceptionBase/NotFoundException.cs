
using System.Net;

namespace BarberBoss.Exception.ExceptionBase;
public class NotFoundException : BarberBossException
{
    public NotFoundException(string errorMessage) : base(errorMessage) { }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
