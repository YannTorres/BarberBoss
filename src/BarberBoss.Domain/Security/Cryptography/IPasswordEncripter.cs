namespace BarberBoss.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    string Encript(string password);

    /// <summary>
    /// Return true if the passwords match, false otherwise.
    /// </summary>
    /// <param name="requestPassword"></param>
    /// <param name="hashPassword"></param>
    /// <returns></returns>
    bool Verify(string requestPassword, string hashPassword);
}
