
using System.Net;

namespace BarberBoss.Exception.ExceptionBase;
public class InvalidLoginException : BarberBossException
{
    public InvalidLoginException() : base(ResourceErrorMessages.INVALID_EMAIL_OR_PASSWORD) { }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
