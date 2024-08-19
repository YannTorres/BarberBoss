using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Incomes;
public interface IIncomeReadOnlyRepository
{
    Task<List<Income>> GetAll();
    Task<Income?> GetById(int id);
    Task<List<Income>> FilterByWeek(DateOnly date);
}
