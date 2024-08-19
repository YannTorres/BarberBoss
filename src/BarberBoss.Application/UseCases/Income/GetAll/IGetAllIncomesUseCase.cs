using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Income.GetAll;
public interface IGetAllIncomesUseCase
{
    public Task<ResponseExpensesJson> Execute();
}
