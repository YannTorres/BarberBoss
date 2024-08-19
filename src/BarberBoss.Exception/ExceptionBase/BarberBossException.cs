namespace BarberBoss.Exception.ExceptionBase;
public abstract class BarberBossException : System.Exception
{
    protected BarberBossException() { }
    protected BarberBossException(string message) : base(message) { }
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
