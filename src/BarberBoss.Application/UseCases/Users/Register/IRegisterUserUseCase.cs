using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
