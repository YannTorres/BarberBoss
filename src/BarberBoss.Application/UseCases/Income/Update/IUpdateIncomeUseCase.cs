using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Income.Update;
public interface IUpdateIncomeUseCase
{
    Task Execute(int id, RequestRegisterIncomeJson request);
}
