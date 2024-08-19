using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Income.Register;
public interface IRegisterIncomeUseCase
{
    Task<ResponseRegisteredIncomeJson> Execute(RequestIncomeJson request);
}
