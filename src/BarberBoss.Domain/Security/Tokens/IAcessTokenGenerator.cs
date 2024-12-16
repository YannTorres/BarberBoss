using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Security.Tokens;
public interface IAcessTokenGenerator
{
    string GenerateToken(User user);
}
