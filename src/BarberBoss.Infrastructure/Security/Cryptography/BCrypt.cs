using BarberBoss.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace BarberBoss.Infrastructure.Security.Cryptography;
internal class BCrypt : IPasswordEncripter
{
    public string Encript(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string requestPassword, string hashPassword)
    {
        return BC.Verify(requestPassword, hashPassword);
    }
}
