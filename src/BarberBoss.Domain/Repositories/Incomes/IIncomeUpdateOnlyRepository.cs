using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Incomes;
public interface IIncomeUpdateOnlyRepository
{
    Task<Income?> GetById(int id);
    void Update(Income income);

}
