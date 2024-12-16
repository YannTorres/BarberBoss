using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Users.Login;
public interface ILoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
