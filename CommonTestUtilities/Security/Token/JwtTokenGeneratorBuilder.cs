using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Security.Token;
public class JwtTokenGeneratorBuilder
{
    public static IAcessTokenGenerator Build()
    {
        var mock = new Mock<IAcessTokenGenerator>();

        mock.Setup(t => t.GenerateToken(It.IsAny<User>())).Returns("GerouBlablablaValido");

        return mock.Object;
    }
}
