using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories.User;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Users.Login;
public class LoginUseCase : ILoginUseCase
{
    private IUserReadOnlyRepository _repositoryReadOnly;
    private IPasswordEncripter _passwordEncripter;
    private IAcessTokenGenerator _acessTokenGenerator;
    public LoginUseCase(
        IUserReadOnlyRepository repositoryReadOnly,
        IPasswordEncripter passwordEncripter,
        IAcessTokenGenerator acessTokenGenerator
        )
    {
        _repositoryReadOnly = repositoryReadOnly;
        _passwordEncripter = passwordEncripter;
        _acessTokenGenerator = acessTokenGenerator;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repositoryReadOnly.GetUserByEmail(request.Email);

        if (user == null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (passwordMatch == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _acessTokenGenerator.GenerateToken(user)
        };
    }
}
