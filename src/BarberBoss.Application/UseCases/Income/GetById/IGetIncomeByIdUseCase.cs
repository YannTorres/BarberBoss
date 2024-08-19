using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Income.GetById;
public interface IGetIncomeByIdUseCase
{
    public Task<ResponseExpenseJson> Execute(int id);
}
