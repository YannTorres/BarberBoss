using BarberBoss.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Security.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(p => p.Encript(It.IsAny<string>())).Returns("PasswordEncriptedBlaBlaBlaQualquerCoisa");

        return mock.Object;
    }
}
